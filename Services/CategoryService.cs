using FluentValidation;
using InventorySystem.Dtos;
using InventorySystem.Exceptions;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Services;

public class CategoryService(DbInitiate db, ILogger<CategoryService> logger, IValidator<CategoryAddRequest> categoryAddValidator, IValidator<CategoryUpdateRequest> categoryUpdateValidator)
{
    private readonly DbInitiate db = db;
    private readonly ILogger<CategoryService> logger = logger;
    private readonly IValidator<CategoryAddRequest> categoryAddValidator = categoryAddValidator;
    private readonly IValidator<CategoryUpdateRequest> categoryUpdateValidator = categoryUpdateValidator;

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

        logger.LogInformation("Category with id {CategoryId} created", category.Id);

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
        }).FirstOrDefaultAsync();

        if (category == null)
        {
            logger.LogWarning("Category with id {CategoryId} not found", id);
            throw new NotFoundException($"category with id {id} not found");
        }

        logger.LogInformation("Category with id {CategoryId} retrieved", id);
        return category;
    }
    // get all categories
    public async Task<List<CategoryResponse>> GetAllCategories()
    {
        var categories = await db.Categories
        .AsNoTracking()
        .Select(c => new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name
        }).ToListAsync();
        logger.LogInformation("All categories retrieved with count {CategoryCount}", categories.Count);
        return categories;
    }
    // update category
    public async Task UpdateCategory(long id, CategoryUpdateRequest request)
    {
        await CategoryValidation(categoryUpdateRequest: request);
        var category = await db.Categories.FindAsync(id);
        if (category == null)
        {
            logger.LogWarning("Category with id {CategoryId} not found", id);
            throw new NotFoundException($"category with id {id} not found");
        }
        category.Name = request.Name;
        await db.SaveChangesAsync();
        logger.LogInformation("Category with id {CategoryId} updated", id);
    }
    // delete category
    public async Task DeleteCategory(long id)
    {
        var category = await db.Categories.FindAsync(id);
        if (category == null)
        {
            logger.LogWarning("Category with id {CategoryId} not found", id);
            throw new NotFoundException($"category with id {id} not found");
        }
        db.Categories.Remove(category);
        await db.SaveChangesAsync();
        logger.LogInformation("Category with id {CategoryId} deleted", id);
    }

    // catgory validation
    private async Task CategoryValidation(CategoryAddRequest? categoryAddRequest = null, CategoryUpdateRequest? categoryUpdateRequest = null)
    {
        if (categoryAddRequest != null)
        {
            var validationResult = await categoryAddValidator.ValidateAsync(categoryAddRequest);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Category add validation failed: {ErrorMessage}", validationResult.Errors.First().ErrorMessage);
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }
        }
        if (categoryUpdateRequest != null)
        {
            var validationResult = await categoryUpdateValidator.ValidateAsync(categoryUpdateRequest);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Category update validation failed: {ErrorMessage}", validationResult.Errors.First().ErrorMessage);
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }
        }
    }
}