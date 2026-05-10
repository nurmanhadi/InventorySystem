using FluentValidation;
using InventorySystem.Dtos;

namespace InventorySystem.Validations;

public class CategoryAddValidation : AbstractValidator<CategoryAddRequest>
{
    public CategoryAddValidation()
    {
        RuleFor(x => x.Name).NotEmpty().Length(2, 100).WithMessage("Category name is required and must be between 2 and 100 characters.");
    }
}

public class CategoryUpdateValidation : AbstractValidator<CategoryUpdateRequest>
{
    public CategoryUpdateValidation()
    {
        RuleFor(x => x.Name).NotEmpty().Length(2, 100).WithMessage("Category name is required and must be between 2 and 100 characters.");
    }
}