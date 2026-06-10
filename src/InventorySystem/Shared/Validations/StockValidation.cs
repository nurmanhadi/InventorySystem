using FluentValidation;
using InventorySystem.Shared.Dtos;

namespace InventorySystem.Shared.Validations;

public class StockValidation : AbstractValidator<StockRequest>
{
    public StockValidation()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
        RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}