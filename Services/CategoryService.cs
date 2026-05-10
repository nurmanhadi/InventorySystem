using FluentValidation;
using InventorySystem.Dtos;
using InventorySystem.Exceptions;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Services;

public class CategoryService(DbInitiate db, IValidator<CategoryAddRequest> categoryAddValidator, IValidator<CategoryUpdateRequest> categoryUpdateValidator)
{
    private readonly IValidator<CategoryAddRequest> categoryAddValidator = categoryAddValidator;
    private readonly IValidator<CategoryUpdateRequest> categoryUpdateValidator = categoryUpdateValidator;
    private readonly DbInitiate db = db;

    // add category
    public async Task<CategoryResponse> AddCategory(CategoryAddRequest request)
    {
        await CategoryValidation(categoryAddRequest: request);
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
        await CategoryValidation(categoryUpdateRequest: request);
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

    // catgory validation
    public async Task CategoryValidation(CategoryAddRequest? categoryAddRequest = null, CategoryUpdateRequest? categoryUpdateRequest = null)
    {
        if (categoryAddRequest != null)
        {
            var validationResult = await categoryAddValidator.ValidateAsync(categoryAddRequest);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }
        }
        if (categoryUpdateRequest != null)
        {
            var validationResult = await categoryUpdateValidator.ValidateAsync(categoryUpdateRequest);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }
        }
    }
}