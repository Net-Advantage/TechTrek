namespace Nabs.TechTrek.Persistence.Entities;

public sealed class TenantEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public TenantIsolationStrategy IsolationStrategy { get; set; }
}
