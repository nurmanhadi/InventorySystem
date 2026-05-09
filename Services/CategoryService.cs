using InventorySystem.Dtos;
using InventorySystem.Exceptions;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Services;

public class CategoryService(DbInitiate db)
{
    private readonly DbInitiate db = db;

    // add category
    public async Task<CategoryResponse> AddCategory(CategoryAddRequest request)
    {
        var category = new Category
        {
            Name = request.Name
        };
        db.Categories.Add(category);
        await db.SaveChangesAsync();

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name
        };
    }
    // get category by id
    public async Task<CategoryResponse> GetCategoryById(long id)
    {
        var category = await db.Categories
        .AsNoTracking()
        .Where(c => c.Id == id)
        .Select(c => new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name
        }).FirstOrDefaultAsync() ?? throw new NotFoundException($"category with id {id} not found");
        return category;
    }
    // get all categories
    public async Task<List<CategoryResponse>> GetAllCategories()
    {
        return await db.Categories
        .AsNoTracking()
        .Select(c => new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name
        }).ToListAsync();
    }
    // update category
    public async Task UpdateCategory(long id, CategoryUpdateRequest request)
    {
        var category = await db.Categories.FindAsync(id) ?? throw new NotFoundException($"category with id {id} not found");
        category.Name = request.Name;
        await db.SaveChangesAsync();
    }
    // delete category
    public async Task DeleteCategory(long id)
    {
        var category = await db.Categories.FindAsync(id) ?? throw new NotFoundException($"category with id {id} not found");
        db.Categories.Remove(category);
        await db.SaveChangesAsync();
    }
}