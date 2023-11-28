namespace Nabs.TechTrek.Persistence.Tenantable;

internal static class TenantQueryExtensions
{
    public static void AddTenantEntityQueryFilter(this IMutableEntityType entityType, ITenantableDbContext tenantableDbContext)
    {
        var tenantIdProperty = entityType.AddProperty("TenantId", typeof(Guid));
        entityType.AddIndex(tenantIdProperty);

        var methodToCall = typeof(TenantQueryExtensions)
               .GetMethod(nameof(SetupTenantQueryFilter),
                   BindingFlags.NonPublic | BindingFlags.Static)!
               .MakeGenericMethod(entityType.ClrType);

        var filter = methodToCall.Invoke(null, [tenantableDbContext]);

        entityType.SetQueryFilter((LambdaExpression)filter!);
    }

    private static LambdaExpression SetupTenantQueryFilter<TEntity>(ITenantableDbContext tenantableDbContext)
           where TEntity : class, ITenantEntity
    {
        Expression<Func<TEntity, bool>> filter = entity =>
            EF.Property<Guid>(entity, "TenantId") == tenantableDbContext.TenantId;

        return filter;
    }
}