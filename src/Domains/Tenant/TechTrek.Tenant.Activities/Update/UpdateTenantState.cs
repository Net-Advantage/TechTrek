using FluentValidation.Results;

namespace TechTrek.Tenant.Activities.Update;

public sealed class UpdateTenantState
        : IActivityState
{
    public Request<Dtos.Tenant>? Request { get; set; }
    public Response<Dtos.Tenant>? Response { get; set; }
    public List<ValidationResult> ValidationResults { get; } = [];

    internal TenantEntity? Entity { get; set; }
}