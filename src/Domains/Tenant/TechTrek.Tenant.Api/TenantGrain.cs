using TechTrek.Tenant.Activities.Add;

namespace TechTrek.Tenant.Api;

public sealed class TenantGrain(
    [PersistentState(stateName: "tenant", storageName: "tenants")]
    IPersistentState<TenantEntity> state)
    : Grain, ITenantGrain
{
    public async Task<Nabs.Application.Response<Dtos.Tenant>> Get()
    {
        var getTenantActivity = new GetTenantActivity(state.State);
        await getTenantActivity.ExecuteAsync();
        return getTenantActivity.State.Response!;
    }

    public async Task<Nabs.Application.Response<Dtos.Tenant>> Add(Dtos.AddTenant addTenant)
    {
        var updateTenantActivity = new AddTenantActivity(addTenant);
        await updateTenantActivity.ExecuteAsync();
        return updateTenantActivity.State.Response!;
    }

    public async Task<Nabs.Application.Response<Dtos.Tenant>> Update(Dtos.Tenant tenant)
    {
        var updateTenantActivity = new UpdateTenantActivity(tenant);
        await updateTenantActivity.ExecuteAsync();
        return updateTenantActivity.State.Response!;
    }

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (state.State is null)
        {
            var entityQueryActivity = new EntityQueryActivity(this.GetPrimaryKey());
            await entityQueryActivity.ExecuteAsync();

            state.State = entityQueryActivity.State.Entity!;
        }
    }
}