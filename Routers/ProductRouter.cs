using InventorySystem.Dtos;
using InventorySystem.Helpers;
using InventorySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Routers;

public static class ProductRouter
{
    public static void MapProductRoutes(this WebApplication app)
    {
        var products = app.MapGroup("/products").WithTags("Product");

        // add product
        products.MapPost("/", async (
                ProductAddRequest request,
                ProductService productService
                ) =>
            {
                var response = await productService.AddProduct(request);
                return Results.Created(
                    $"/api/products/{response.Id}",
                    new WebResponse<ProductResponse>(message: "Product created successfully", data: response));
            });
        // get product by id
        products.MapGet("/{id}", async (
            long id,
            ProductService productService
            ) =>
        {
            var response = await productService.GetProductById(id);
            return Results.Ok(new WebResponse<ProductResponse>(message: "Product retrieved successfully", data: response));
        });
        // get all products
        products.MapGet("/", async (
            [FromServices] ProductService productService,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10
            ) =>
        {
            var response = await productService.GetAllProducts(page, size);
            return Results.Ok(new WebResponse<WebPaginationResponse<ProductResponse>>(message: "Products retrieved successfully", data: response));
        });
        // update product
        products.MapPut("/{id}", async (
            long id,
            ProductUpdateRequest request,
            ProductService productService
            ) =>
        {
            await productService.UpdateProduct(id, request);
            return Results.Ok(new WebResponse<ProductResponse>(message: "Product updated successfully"));
        });
        // delete product
        products.MapDelete("/{id}", async (
            long id,
            ProductService productService
            ) =>
        {
            await productService.DeleteProduct(id);
            return Results.Ok(new WebResponse<ProductResponse>(message: "Product deleted successfully"));
        });
    }
}