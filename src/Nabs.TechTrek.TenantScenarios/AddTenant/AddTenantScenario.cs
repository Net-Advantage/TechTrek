namespace Nabs.TechTrek.TenantScenarios.AddTenant;

public sealed class AddTenantScenario
    : ScenarioBase<AddTenantRequest, AddTenantResponse, AddTenantActivityState>
{
    public AddTenantScenario(
        IApplicationContext applicationContext)
        : base(applicationContext)
    {
    }

    protected override async Task<AddTenantResponse> InvokeActivity(AddTenantRequest request)
    {
        var result = new AddTenantResponse();

        await Task.CompletedTask;
        return result;
    }

    protected override AddTenantResponse ProcessResult()
    {
        throw new NotImplementedException();
    }
}

public sealed record AddTenantRequest : IRequest<AddTenantResponse>, IProjection;

public record AddTenantResponse : IProjection
{

}

public sealed record AddTenantActivityState : ActivityState
{

}