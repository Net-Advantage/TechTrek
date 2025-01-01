namespace TechTrek.Tenant.Api;

[Controller]
[Route("api/core/[controller]")]
public class TenantController(IGrainFactory grainFactory) : ControllerBase
{
    [HttpGet("{tenantId:guid}")]
    public async Task<ActionResult<Dtos.Tenant>> Get(Guid tenantId)
    {
        var tenantGrain = grainFactory.GetGrain<ITenantGrain>(tenantId);
        var tenant = await tenantGrain.Get();
        return tenant is null
            ? NotFound()
            : Ok(tenant);
    }
}
