namespace Nabs.TechTrek.Persistence;

public abstract class EntityBase<TId>
{
    public TId Id { get; set; } = default!;
}
