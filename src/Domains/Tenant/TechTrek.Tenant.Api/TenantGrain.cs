namespace TechTrek.Tenant.Api;

public sealed class TenantGrain(
        [PersistentState(stateName: "tenant", storageName: "tenants")]
        IPersistentState<Dtos.Tenant> state,
        TenantActivityFactory tenantActivityFactory)
    : Grain, ITenantGrain
{
    private readonly TenantActivityFactory _tenantActivityFactory = tenantActivityFactory;

    public Task<Dtos.Tenant> Update(Dtos.Tenant tenant)
    {
        state.State = tenant;

        return Task.FromResult(state.State);
    }

    public Task<Dtos.Tenant> Get()
    {
        return Task.FromResult(state.State);
    }

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (state.State is null)
        {
            var getActivity = _tenantActivityFactory.GetActivity();
            var primaryKey = this.GetPrimaryKey();
            var tenant = await getActivity.ExecuteAsync(primaryKey, cancellationToken);
            state.State = tenant;
        }

    }
}