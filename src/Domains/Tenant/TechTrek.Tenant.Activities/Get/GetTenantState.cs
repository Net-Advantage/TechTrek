using FluentValidation.Results;

namespace TechTrek.Tenant.Activities.Get;

public class GetTenantState : IActivityState
{
    public Response<Dtos.Tenant>? Response { get; set; }
    public List<ValidationResult> ValidationResults { get; } = [];
    internal TenantEntity? Entity { get; set; }
    internal Dtos.Tenant? Dto { get; set; }
}
