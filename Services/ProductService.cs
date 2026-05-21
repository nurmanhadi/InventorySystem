using FluentValidation;
using InventorySystem.Dtos;
using InventorySystem.Exceptions;
using InventorySystem.Helpers;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Services;

class ProductService(DbInitiate db, ILogger<ProductService> logger, IValidator<ProductAddRequest> productAddValidator, IValidator<ProductUpdateRequest> productUpdateValidator)
{
    private readonly DbInitiate db = db;
    private readonly ILogger<ProductService> logger = logger;
    private readonly IValidator<ProductAddRequest> productAddValidator = productAddValidator;
    private readonly IValidator<ProductUpdateRequest> productUpdateValidator = productUpdateValidator;

    // add product
    public async Task<ProductResponse> AddProduct(ProductAddRequest request)
    {
        await ProductValidation(productAddRequest: request);
        await CheckCategoryExistsAsync(request.CategoryId);
        await CheckSkuExistsAsync(request.Sku);
        var product = new Product
        {
            Name = request.Name,
            Sku = request.Sku,
            Stock = request.Stock,
            Price = request.Price,
            CreatedAt = DateTime.UtcNow,
            CategoryId = request.CategoryId
        };

        db.Products.Add(product);
        await db.SaveChangesAsync();
        logger.LogInformation("Product with id {ProductId} created", product.Id);

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku,
            Stock = product.Stock,
            Price = product.Price,
            CreatedAt = product.CreatedAt,
            CategoryId = product.CategoryId
        };
    }
    // get product by id
    public async Task<ProductWithCategoryResponse> GetProductById(long id)
    {
        var product = await db.Products
        .AsNoTracking()
        .Where(p => p.Id == id)
        .Select(p => new ProductWithCategoryResponse
        {
            Id = p.Id,
            Name = p.Name,
            Sku = p.Sku,
            Stock = p.Stock,
            Price = p.Price,
            CreatedAt = p.CreatedAt,
            CategoryId = p.CategoryId,
            Category = new CategoryResponse
            {
                Id = p.Category!.Id,
                Name = p.Category!.Name
            }
        }).FirstOrDefaultAsync();
        if (product == null)
        {
            logger.LogWarning("Product with id {ProductId} not found", id);
            throw new NotFoundException($"Product with id {id} not found");
        }
        logger.LogInformation("Product with id {ProductId} retrieved", id);
        return product;
    }
    // get all products
    public async Task<WebPaginationResponse<ProductResponse>> GetAllProducts(int page, int pageSize, string? search = null, long? categoryId = null)
    {
        var totalItemsQuery = db.Products.AsNoTracking().AsQueryable();
        var query = db.Products
        .AsNoTracking()
        .AsQueryable();
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.SearchVerctor.Matches(search));
            totalItemsQuery = totalItemsQuery.Where(p => p.SearchVerctor.Matches(search));
        }
        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
            totalItemsQuery = totalItemsQuery.Where(p => p.CategoryId == categoryId.Value);
        }
        var totalItems = await totalItemsQuery.CountAsync();
        var products = await query
        .OrderByDescending(p => p.CreatedAt)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(p => new ProductResponse
        {
            Id = p.Id,
            Name = p.Name,
            Sku = p.Sku,
            Stock = p.Stock,
            Price = p.Price,
            CreatedAt = p.CreatedAt,
            CategoryId = p.CategoryId
        }).ToListAsync();
        logger.LogInformation("Products retrieved with count {ProductCount}, page {Page}, pageSize {PageSize}", products.Count, page, pageSize);
        return new WebPaginationResponse<ProductResponse>(products, page, pageSize, totalItems);
    }
    // update product
    public async Task UpdateProduct(long id, ProductUpdateRequest request)
    {
        await ProductValidation(productUpdateRequest: request);
        var product = await db.Products.FindAsync(id);
        if (product == null)
        {
            logger.LogWarning("Product with id {ProductId} not found", id);
            throw new NotFoundException($"Product with id {id} not found");
        }
        if (!string.IsNullOrEmpty(request.Name))
        {
            product.Name = request.Name;
        }
        if (!string.IsNullOrEmpty(request.Sku))
        {
            await CheckSkuExistsAsync(request.Sku);
            product.Sku = request.Sku;
        }
        if (request.Stock.HasValue)
        {
            product.Stock = request.Stock.Value;
        }
        if (request.Price.HasValue)
        {
            product.Price = request.Price.Value;
        }
        if (request.CategoryId.HasValue)
        {
            await CheckCategoryExistsAsync(request.CategoryId.Value);
            product.CategoryId = request.CategoryId.Value;
        }
        await db.SaveChangesAsync();
        logger.LogInformation("Product with id {ProductId} updated", id);
    }
    // delete product
    public async Task DeleteProduct(long id)
    {
        var product = await db.Products.FindAsync(id);
        if (product == null)
        {
            logger.LogWarning("Product with id {ProductId} not found", id);
            throw new NotFoundException($"Product with id {id} not found");
        }
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        logger.LogInformation("Product with id {ProductId} deleted", id);
    }

    // check category exists
    private async Task CheckCategoryExistsAsync(long categoryId)
    {
        var category = await db.Categories.CountAsync(c => c.Id == categoryId);
        if (category == 0)
        {
            logger.LogWarning("Category with id {CategoryId} not found", categoryId);
            throw new NotFoundException($"Category with id {categoryId} not found");
        }
    }
    // check sku exists
    private async Task CheckSkuExistsAsync(string sku)
    {
        var product = await db.Products.CountAsync(p => p.Sku == sku);
        if (product > 0)
        {
            logger.LogWarning("Product with sku {Sku} already exists", sku);
            throw new BadRequestException($"Product with sku {sku} already exists");
        }
    }
    // product validation
    private async Task ProductValidation(ProductAddRequest? productAddRequest = null, ProductUpdateRequest? productUpdateRequest = null)
    {
        if (productAddRequest != null)
        {
            var validationResult = await productAddValidator.ValidateAsync(productAddRequest);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Product add validation failed: {ErrorMessage}", validationResult.Errors.First().ErrorMessage);
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }
        }
        if (productUpdateRequest != null)
        {
            var validationResult = await productUpdateValidator.ValidateAsync(productUpdateRequest);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Product update validation failed: {ErrorMessage}", validationResult.Errors.First().ErrorMessage);
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }
        }
    }
}