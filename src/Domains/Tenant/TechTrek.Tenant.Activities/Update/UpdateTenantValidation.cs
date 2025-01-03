namespace TechTrek.Tenant.Activities.Update;

internal sealed class UpdateTenantValidation : AbstractValidator<Dtos.AddTenant>
{
    public UpdateTenantValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Tenant Name is required.")
            .MaximumLength(100)
            .WithMessage("Tenant Name must be less than 100 characters.");
    }
}
