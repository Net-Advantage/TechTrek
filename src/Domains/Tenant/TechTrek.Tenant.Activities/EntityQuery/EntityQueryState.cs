using FluentValidation.Results;

namespace TechTrek.Tenant.Activities.EntityQuery;

public sealed class EntityQueryState
     : IActivityState
{
    public Guid TenantId { get; set; }
    public TenantEntity? Entity { get; set; } = default!;

    public List<ValidationResult> ValidationResults { get; } = [];
}
