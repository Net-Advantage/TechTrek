using FluentValidation;

namespace TechTrek.Tenant.Dtos;

public class AddTenantValidation : AbstractValidator<AddTenant>
{
    public AddTenantValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Tenant Name is required.")
            .MaximumLength(100)
            .WithMessage("Tenant Name must be less than 100 characters.");
    }
}
