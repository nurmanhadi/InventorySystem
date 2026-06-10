using FluentValidation;
using InventorySystem.Shared.Dtos;

namespace InventorySystem.Shared.Validations;

public class ProductAddValidation : AbstractValidator<ProductAddRequest>
{
    public ProductAddValidation()
    {
        RuleFor(x => x.Name).NotEmpty().Length(2, 100).WithMessage("Product name is required and must be between 2 and 100 characters.");
        RuleFor(x => x.Sku).NotEmpty().Length(2, 50).WithMessage("Product SKU is required and must be between 2 and 50 characters.");
        RuleFor(x => x.Stock).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Product stock must be greater than or equal to 0.");
        RuleFor(x => x.Price).NotEmpty().GreaterThan(0).WithMessage("Product price must be greater than 0.");
        RuleFor(x => x.CategoryId).NotEmpty().GreaterThan(0).WithMessage("Category id must be greater than 0.");
    }
}
public class ProductUpdateValidation : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateValidation()
    {
        RuleFor(x => x.Name).Length(2, 100).WithMessage("Product name is required and must be between 2 and 100 characters.");
        RuleFor(x => x.Sku).Length(2, 50).WithMessage("Product SKU is required and must be between 2 and 50 characters.");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Product stock must be greater than or equal to 0.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than 0.");
        RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Category id must be greater than 0.");
    }
}