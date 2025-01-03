namespace TechTrek.Tenant.Activities.EntityPersist;

public sealed class EntityPersistActivity
    : Activity<EntityPersistState>
{
    public EntityPersistActivity()
    {
        AddValidator<EntityPersistValidator>(v => v.Entity!);
        AddUnitOfWork<EntityPersistUnityOfWork>();
    }
}

public sealed class EntityPersistValidator
    : AbstractValidator<TenantEntity>
{
    public EntityPersistValidator()
    {
        RuleFor(s => s)
            .NotNull();

        RuleFor(s => s.Id)
            .NotEmpty();
    }
}