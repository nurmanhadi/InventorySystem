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
            [FromServices] ProductService productService,
            [FromBody] ProductAddRequest request
            ) =>
        {
            var response = await productService.AddProduct(request);
            return Results.Created(
                $"/api/products/{response.Id}",
                new WebResponse<ProductResponse>(message: "Product created successfully", data: response));
        })
        .RequireAuthorization(RolePolicy.AdminOnly.ToString())
        .Produces<WebResponse<ProductResponse>>(201)
        .Produces<WebResponse<string>>(400)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);

        // get product by id
        products.MapGet("/{id}", async (
            [FromServices] ProductService productService,
            [FromRoute] long id
            ) =>
        {
            var response = await productService.GetProductById(id);
            return Results.Ok(new WebResponse<ProductWithCategoryResponse>(message: "Product retrieved successfully", data: response));
        })
        .RequireAuthorization(RolePolicy.WarehouseOperations.ToString())
        .Produces<WebResponse<ProductWithCategoryResponse>>(200)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);

        // get all products
        products.MapGet("/", async (
            [FromServices] ProductService productService,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] string? search = null,
            [FromQuery] long? categoryId = null
            ) =>
        {
            var response = await productService.GetAllProducts(page, size, search, categoryId);
            return Results.Ok(new WebResponse<WebPaginationResponse<ProductResponse>>(message: "Products retrieved successfully", data: response));
        })
        .RequireAuthorization(RolePolicy.WarehouseOperations.ToString())
        .Produces<WebResponse<WebPaginationResponse<ProductResponse>>>(200)
        .Produces<WebResponse<string>>(400)
        .Produces<WebResponse<string>>(500);

        // update product
        products.MapPut("/{id}", async (
            [FromServices] ProductService productService,
            [FromRoute] long id,
            [FromBody] ProductUpdateRequest request
            ) =>
        {
            await productService.UpdateProduct(id, request);
            return Results.Ok(new WebResponse<ProductResponse>(message: "Product updated successfully"));
        })
        .RequireAuthorization(RolePolicy.AdminOnly.ToString())
        .Produces<WebResponse<ProductResponse>>(200)
        .Produces<WebResponse<string>>(400)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);

        // delete product
        products.MapDelete("/{id}", async (
            [FromServices] ProductService productService,
            [FromRoute] long id
            ) =>
        {
            await productService.DeleteProduct(id);
            return Results.Ok(new WebResponse<ProductResponse>(message: "Product deleted successfully"));
        })
        .RequireAuthorization(RolePolicy.AdminOnly.ToString())
        .Produces<WebResponse<ProductResponse>>(200)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);
    }
}