using InventorySystem.Dtos;
using InventorySystem.Helpers;
using InventorySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Routers;

public static class CategoryRouter
{
    public static void MapCategoryRoutes(this WebApplication app)
    {
        var category = app.MapGroup("/categories").WithTags("Category");

        // add category
        category.MapPost("/", async (
            [FromServices] CategoryService categoryService,
            [FromBody] CategoryAddRequest request
            ) =>
        {
            var response = await categoryService.AddCategory(request);
            return Results.Created(
                $"/api/categories/{response.Id}",
                new WebResponse<CategoryResponse>(message: "Category created successfully", data: response));
        })
        .Produces<WebResponse<CategoryResponse>>(201)
        .Produces<WebResponse<string>>(400)
        .Produces<WebResponse<string>>(500);

        // get category by id
        category.MapGet("/{id}", async (
            [FromServices] CategoryService categoryService,
            [FromRoute] int id
            ) =>
        {
            var response = await categoryService.GetCategoryById(id);
            return Results.Ok(new WebResponse<CategoryResponse>(message: "Category retrieved successfully", data: response));
        })
        .Produces<WebResponse<CategoryResponse>>(200)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);

        // get all categories
        category.MapGet("/", async (
            [FromServices] CategoryService categoryService
            ) =>
        {
            var response = await categoryService.GetAllCategories();
            return Results.Ok(new WebResponse<List<CategoryResponse>>(message: "Categories retrieved successfully", data: response));
        })
        .Produces<WebResponse<List<CategoryResponse>>>(200)
        .Produces<WebResponse<string>>(500);

        // update category
        category.MapPut("/{id}", async (
            [FromServices] CategoryService categoryService,
            [FromRoute] int id,
            [FromBody] CategoryUpdateRequest request
            ) =>
        {
            await categoryService.UpdateCategory(id, request);
            return Results.Ok(new WebResponse<object>(message: "Category updated successfully"));
        })
        .Produces<WebResponse<object>>(200)
        .Produces<WebResponse<string>>(400)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);

        // delete category
        category.MapDelete("/{id}", async (
            [FromServices] CategoryService categoryService,
            [FromRoute] int id
            ) =>
        {
            await categoryService.DeleteCategory(id);
            return Results.Ok(new WebResponse<object>(message: "Category deleted successfully"));
        })
        .Produces<WebResponse<object>>(200)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);
    }
}