using InventorySystem.Dtos;
using InventorySystem.Exceptions;
using InventorySystem.Helpers;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Services;

class ProductService(DbInitiate db)
{
    private readonly DbInitiate db = db;

    // add product
    public async Task<ProductResponse> AddProduct(ProductAddRequest request)
    {
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
        }).FirstOrDefaultAsync() ?? throw new NotFoundException($"Product with id {id} not found");
        return product;
    }
    // get all products
    public async Task<WebPaginationResponse<ProductResponse>> GetAllProducts(int page, int pageSize, string? search = null)
    {
        var totalItemsQuery = db.Products.AsNoTracking().AsQueryable();
        var query = db.Products
        .AsNoTracking()
        .AsQueryable();
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.Contains(search) || p.Sku.Contains(search));
            totalItemsQuery = totalItemsQuery.Where(p => p.Name.Contains(search) || p.Sku.Contains(search));
        }
        ;
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

        return new WebPaginationResponse<ProductResponse>(products, page, pageSize, totalItems);
    }
    // update product
    public async Task UpdateProduct(long id, ProductUpdateRequest request)
    {
        var product = await db.Products.FindAsync(id) ?? throw new NotFoundException($"Product with id {id} not found");
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
    }
    // delete product
    public async Task DeleteProduct(long id)
    {
        var product = await db.Products.FindAsync(id) ?? throw new NotFoundException($"Product with id {id} not found");
        db.Products.Remove(product);
        await db.SaveChangesAsync();
    }

    // check category exists
    private async Task CheckCategoryExistsAsync(long categoryId)
    {
        var category = await db.Categories.CountAsync(c => c.Id == categoryId);
        if (category == 0)
        {
            throw new NotFoundException($"Category with id {categoryId} not found");
        }
    }
    // check sku exists
    private async Task CheckSkuExistsAsync(string sku)
    {
        var product = await db.Products.CountAsync(p => p.Sku == sku);
        if (product > 0)
        {
            throw new BadRequestException($"Product with sku {sku} already exists");
        }
    }
}