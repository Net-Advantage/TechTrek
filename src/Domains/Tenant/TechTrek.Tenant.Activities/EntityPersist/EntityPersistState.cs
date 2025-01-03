using FluentValidation.Results;

namespace TechTrek.Tenant.Activities.EntityPersist;

public sealed class EntityPersistState
    : IActivityState
{
    internal TenantEntity? Entity { get; set; }

    public List<ValidationResult> ValidationResults { get; } = [];
}