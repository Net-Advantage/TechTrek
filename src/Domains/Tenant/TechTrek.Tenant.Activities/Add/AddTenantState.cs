using FluentValidation.Results;

namespace TechTrek.Tenant.Activities.Add;

public sealed class AddTenantState 
    : IActivityState<AddTenant, Dtos.Tenant>
{
    public Request<AddTenant>? Request { get; set; }
    public Response<Dtos.Tenant>? Response { get; set; }

    public List<ValidationResult> ValidationResults { get; } = [];

    internal TenantEntity? Entity { get; set; }
}
