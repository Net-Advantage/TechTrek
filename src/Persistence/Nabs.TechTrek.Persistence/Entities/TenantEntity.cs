namespace Nabs.TechTrek.Persistence.Entities;

public sealed class TenantEntity : EntityBase<Guid>, ITenantEntity
{
    public string Name { get; set; } = default!;
    public TenantIsolationStrategy IsolationStrategy { get; set; }
}