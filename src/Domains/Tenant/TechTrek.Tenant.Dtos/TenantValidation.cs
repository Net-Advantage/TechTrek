using FluentValidation;

namespace TechTrek.Tenant.Dtos;

public class TenantValidation : AbstractValidator<Tenant>
{
    public TenantValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Tenant Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Tenant Name is required.")
            .MaximumLength(100)
            .WithMessage("Tenant Name must be less than 100 characters.");
    }
}
