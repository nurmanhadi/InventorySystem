using FluentValidation;
using InventorySystem.Shared.Dtos;

namespace InventorySystem.Shared.Validations;

public class SupplierAddValidation : AbstractValidator<SupplierAddRequest>
{
    public SupplierAddValidation()
    {
        RuleFor(x => x.Name).NotEmpty().Length(2, 20).WithMessage("name required at least lengts 2 to 20");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(2, 100).WithMessage("email required at least lengts 100");
        RuleFor(x => x.Phone).Length(2, 20).WithMessage("phone at least lengts 2 to 20");
        RuleFor(x => x.Address).Length(2, 500).WithMessage("email at least lengts 2 to 500");
    }
}

public class SupplierUpdateValidation : AbstractValidator<SupplierUpdateRequest>
{
    public SupplierUpdateValidation()
    {
        RuleFor(x => x.Name).Length(2, 20).WithMessage("name at least lengts 2 to 20");
        RuleFor(x => x.Email).EmailAddress().Length(2, 100).WithMessage("email at least lengts 100");
        RuleFor(x => x.Phone).Length(2, 20).WithMessage("phone at least lengts 2 to 20");
        RuleFor(x => x.Address).Length(2, 500).WithMessage("email at least lengts 2 to 500");
    }
}