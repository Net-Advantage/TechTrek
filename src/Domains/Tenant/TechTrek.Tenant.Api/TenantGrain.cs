using Ardalis.Result;

namespace TechTrek.Tenant.Api;

public sealed class TenantGrain(
    [PersistentState(stateName: "tenant", storageName: "tenants")]
    IPersistentState<TenantEntity> state)
    : Grain, ITenantGrain
{
    public async Task<Result<Dtos.Tenant>> Get()
    {
        var getTenantActivity = new GetTenantActivity(state.State);
        var result = await getTenantActivity.ExecuteAsync();
        return result.Value.Out!;
    }

    public async Task<Result<Dtos.Tenant>> Add(Dtos.AddTenant addTenant)
    {
        var updateTenantActivity = new AddTenantActivity(addTenant);
        var result = await updateTenantActivity.ExecuteAsync();
        return result.Value.Out!;
    }

    public async Task<Result<Dtos.Tenant>> Update(Dtos.Tenant tenant)
    {
        var updateTenantActivity = new UpdateTenantActivity(tenant);
        var result = await updateTenantActivity.ExecuteAsync();
        return result.Value.Out!;
    }

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (state.State is null)
        {
            var entityQueryActivity = new EntityQueryActivity(this.GetPrimaryKey());
            var result = await entityQueryActivity.ExecuteAsync();

            state.State = result.Value.Entity!;
        }
    }
}