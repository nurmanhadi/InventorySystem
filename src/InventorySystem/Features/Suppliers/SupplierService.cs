using FluentValidation;
using InventorySystem.Infrastructure.Databases;
using InventorySystem.Infrastructure.Models;
using InventorySystem.Shared.Dtos;
using InventorySystem.Shared.Exceptions;
using InventorySystem.Shared.Helpers;
using InventorySystem.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Features.Suppliers;

public class SupplierService(
    DbInitiate _db,
    ILogger<SupplierService> _logger,
    IValidator<SupplierAddRequest> _supplierAddValidator,
    IValidator<SupplierUpdateRequest> _supplierUpdateValidator
)
{
    // add supplier
    public async Task<SupplierResponse> AddSupplier(SupplierAddRequest request)
    {
        await SupplierValidation(supplierAddRequest: request);
        await SupplierCheckEmailExists(request.Email);
        var supplierCode = SupplierCode.Generate();

        var supplier = new Supplier
        {
            Code = supplierCode,
            Name = request.Name,
            Email = request.Email
        };
        if (!string.IsNullOrEmpty(request.Phone))
        {
            await SupplierCheckPhoneExists(request.Phone);
            supplier.Phone = request.Phone;
        }
        if (!string.IsNullOrEmpty(request.Address))
        {
            supplier.Address = request.Address;
        }

        _db.Suppliers.Add(supplier);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Supplier with id {SupplierId} created", supplier.Id);
        return new SupplierResponse
        {
            Id = supplier.Id,
            Code = supplier.Code,
            Name = supplier.Name,
            Email = supplier.Email,
            Phone = supplier.Phone,
            Address = supplier.Address,
            IsActive = supplier.IsActive
        };
    }

    // update supplier
    public async Task UpdateSupplier(long id, SupplierUpdateRequest request)
    {
        await SupplierValidation(supplierUpdateRequest: request);
        var supplier = await _db.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            _logger.LogWarning("Supplier with id {SupplierId} not found", id);
            throw new NotFoundException("Supplier not found");
        }
        if (!string.IsNullOrEmpty(request.Name))
        {
            supplier.Name = request.Name;
        }
        if (!string.IsNullOrEmpty(request.Email))
        {
            await SupplierCheckEmailExists(request.Email);
            supplier.Email = request.Email;
        }
        if (!string.IsNullOrEmpty(request.Phone))
        {
            await SupplierCheckPhoneExists(request.Phone);
            supplier.Phone = request.Phone;
        }
        if (!string.IsNullOrEmpty(request.Address))
        {
            supplier.Address = request.Address;
        }
        if (request.IsActive.HasValue)
        {
            supplier.IsActive = request.IsActive.Value;
        }
        await _db.SaveChangesAsync();

        _logger.LogInformation("Supplier with id {SupplierId} updated", supplier.Id);
    }

    // get all suppliers
    public async Task<WebPaginationResponse<SupplierResponse>> GetSuppliers(
        int page,
        int size,
        OrderBy? orderBy,
        SupplierActive? supplierActive,
        SupplierOrderBy? supplierOrderBy
        )
    {
        var querySupplier = _db.Suppliers.AsNoTracking().AsQueryable();
        if (supplierActive != null)
        {
            switch (supplierActive)
            {
                case SupplierActive.Active:
                    querySupplier = querySupplier.Where(x => x.IsActive == true);
                    break;
                case SupplierActive.Nonactive:
                    querySupplier = querySupplier.Where(x => x.IsActive == false);
                    break;
            }
        }
        if (supplierOrderBy != null)
        {
            switch (supplierOrderBy)
            {
                case SupplierOrderBy.Code:
                    querySupplier = orderBy == OrderBy.Ascending
                        ? querySupplier.OrderBy(x => x.Code)
                        : querySupplier.OrderByDescending(x => x.Code);
                    break;
                case SupplierOrderBy.Name:
                    querySupplier = orderBy == OrderBy.Ascending
                        ? querySupplier.OrderBy(x => x.Name)
                        : querySupplier.OrderByDescending(x => x.Name);
                    break;
                case SupplierOrderBy.Email:
                    querySupplier = orderBy == OrderBy.Ascending
                        ? querySupplier.OrderBy(x => x.Email)
                        : querySupplier.OrderByDescending(x => x.Email);
                    break;
                case SupplierOrderBy.Phone:
                    querySupplier = orderBy == OrderBy.Ascending
                        ? querySupplier.OrderBy(x => x.Phone)
                        : querySupplier.OrderByDescending(x => x.Phone);
                    break;
            }
        }
        else
        {
            querySupplier = orderBy == OrderBy.Ascending ? querySupplier.OrderBy(x => x.CreatedAt) : querySupplier.OrderByDescending(x => x.CreatedAt);
        }

        var totalSupplier = await querySupplier.AsNoTracking().CountAsync();

        var suppliers = await querySupplier
        .Skip((page - 1) * size)
        .Take(size)
        .Select(x => new SupplierResponse
        {
            Id = x.Id,
            Code = x.Code,
            Name = x.Name,
            Email = x.Email,
            Phone = x.Phone,
            Address = x.Address,
            IsActive = x.IsActive
        })
        .ToListAsync();

        _logger.LogInformation("All supplier retrieved with count {SupplierCount}", suppliers.Count);
        return new WebPaginationResponse<SupplierResponse>(suppliers, page, size, totalSupplier);
    }

    // get supplier
    public async Task<SupplierResponse> GetSupplier(long id)
    {
        var supplier = await _db.Suppliers
        .AsNoTracking()
        .Where(x => x.Id == id)
        .Select(x => new SupplierResponse
        {
            Id = x.Id,
            Code = x.Code,
            Name = x.Name,
            Email = x.Email,
            Phone = x.Phone,
            Address = x.Address,
            IsActive = x.IsActive
        })
        .FirstOrDefaultAsync();
        if (supplier == null)
        {
            _logger.LogWarning("Supplier with id {SupplierId} not found", id);
            throw new NotFoundException("supplier not found");
        }
        _logger.LogInformation("supplier retrieved with id {SupplierId}", id);
        return supplier;
    }

    // delete supplier
    public async Task DeleteSupplier(long id)
    {
        var supplier = await _db.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            _logger.LogWarning("Supplier with id {SupplierId} not found", id);
            throw new NotFoundException("supplier not found");
        }

        if (supplier.IsActive == true)
        {
            supplier.IsActive = false;
        }
        supplier.DeletedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        _logger.LogInformation("supplier with id {SupplierId} deleted", id);
    }

    // check email already exists
    private async Task SupplierCheckEmailExists(string email)
    {
        var countEmail = await _db.Suppliers.AsNoTracking().CountAsync(x => x.Email == email);
        if (countEmail > 0)
        {
            _logger.LogWarning("supplier email {Email} already exists", email);
            throw new BadRequestException($"supplier email {email} already exists");
        }
    }

    // check phone already exists
    private async Task SupplierCheckPhoneExists(string phone)
    {
        var countEmail = await _db.Suppliers.AsNoTracking().CountAsync(x => x.Phone == phone);
        if (countEmail > 0)
        {
            _logger.LogWarning("supplier phone {Phone} already exists", phone);
            throw new BadRequestException($"supplier phone {phone} already exists");
        }
    }

    // validation
    private async Task SupplierValidation(
        SupplierAddRequest? supplierAddRequest = null,
        SupplierUpdateRequest? supplierUpdateRequest = null
    )
    {
        if (supplierAddRequest != null)
        {
            var validationResult = await _supplierAddValidator.ValidateAsync(supplierAddRequest);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Supplier validation failed: {Errors}", validationResult.Errors.First().ErrorMessage);
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }
        }
        if (supplierUpdateRequest != null)
        {
            var validationResult = await _supplierUpdateValidator.ValidateAsync(supplierUpdateRequest);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Supplier validation failed: {Errors}", validationResult.Errors.First().ErrorMessage);
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }
        }
    }
}