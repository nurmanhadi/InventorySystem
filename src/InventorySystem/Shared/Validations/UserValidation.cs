using FluentValidation;
using InventorySystem.Shared.Dtos;

namespace InventorySystem.Shared.Validations;

public class UserAddValidation : AbstractValidator<UserAddRequest>
{
    public UserAddValidation()
    {
        RuleFor(x => x.Username).NotEmpty().Length(3, 100).WithMessage("Username is required and must be between 3 and 100 characters.");
        RuleFor(x => x.Password).NotEmpty().Length(6, 255).WithMessage("Password is required and must be between 6 and 255 characters long.");
        RuleFor(x => x.Role).NotEmpty().IsInEnum().WithMessage("Role must be a valid enum value.");
    }
}

public class UserUpdateValidation : AbstractValidator<UserUpdateRequest>
{
    public UserUpdateValidation()
    {
        RuleFor(x => x.Username).Length(3, 100).When(x => !string.IsNullOrEmpty(x.Username)).WithMessage("Username must be between 3 and 100 characters.");
        RuleFor(x => x.Password).Length(6, 255).When(x => !string.IsNullOrEmpty(x.Password)).WithMessage("Password must be between 6 and 255 characters long.");
        RuleFor(x => x.Role).IsInEnum().When(x => x.Role.HasValue).WithMessage("Role must be a valid enum value.");
    }
}