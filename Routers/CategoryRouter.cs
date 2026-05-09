using InventorySystem.Dtos;
using InventorySystem.Helpers;
using InventorySystem.Services;

namespace InventorySystem.Routers;

public static class CategoryRouter
{
    public static void MapCategoryRoutes(this WebApplication app)
    {
        var category = app.MapGroup("/api/category").WithTags("Category");

        // add category
        category.MapPost("/", async (
            CategoryAddRequest request,
            CategoryService categoryService
            ) =>
        {
            var response = await categoryService.AddCategory(request);
            return Results.Created(
                $"/api/category/{response.Id}",
                new WebResponse<CategoryResponse>(message: "Category created successfully", data: response));
        });
        // get category by id
        category.MapGet("/{id}", async (
            int id,
            CategoryService categoryService
            ) =>
        {
            var response = await categoryService.GetCategoryById(id);
            return Results.Ok(new WebResponse<CategoryResponse>(message: "Category retrieved successfully", data: response));
        });
        // get all categories
        category.MapGet("/", async (CategoryService categoryService) =>
        {
            var response = await categoryService.GetAllCategories();
            return Results.Ok(new WebResponse<List<CategoryResponse>>(message: "Categories retrieved successfully", data: response));
        });
        // update category
        category.MapPut("/{id}", async (
            int id,
            CategoryUpdateRequest request,
            CategoryService categoryService
            ) =>
        {
            await categoryService.UpdateCategory(id, request);
            return Results.Ok(new WebResponse<object>(message: "Category updated successfully"));
        });
        // delete category
        category.MapDelete("/{id}", async (
            int id,
            CategoryService categoryService
            ) =>
        {
            await categoryService.DeleteCategory(id);
            return Results.Ok(new WebResponse<object>(message: "Category deleted successfully"));
        });
    }
}