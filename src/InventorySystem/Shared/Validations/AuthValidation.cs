using FluentValidation;
using InventorySystem.Shared.Dtos;

namespace InventorySystem.Shared.Validations;

public class AuthLoginValidation : AbstractValidator<AuthLoginRequest>
{
    public AuthLoginValidation()
    {
        RuleFor(x => x.Username).NotEmpty().Length(3, 100).WithMessage("Username is required and must be between 3 and 100 characters.");
        RuleFor(x => x.Password).NotEmpty().Length(6, 255).WithMessage("Password is required and must be between 6 and 255 characters.");
    }
}