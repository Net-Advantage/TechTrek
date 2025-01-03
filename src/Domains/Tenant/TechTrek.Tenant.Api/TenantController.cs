using TechTrek.Tenant.Dtos;

namespace TechTrek.Tenant.Api;

[Controller]
[Route("api/core/[controller]")]
public class TenantController(IGrainFactory grainFactory) : ControllerBase
{
    [HttpGet("{tenantId:guid}")]
    public async Task<ActionResult<Dtos.AddTenant>> Get([FromRoute]Guid tenantId)
    {
        var tenantGrain = grainFactory.GetGrain<ITenantGrain>(tenantId);
        var response = await tenantGrain.Get();
        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpPut()]
    public async Task<ActionResult<Dtos.AddTenant>> Put([FromBody]AddTenant addTenant)
    {
        var tenantId = Guid.NewGuid();
        var tenantGrain = grainFactory.GetGrain<ITenantGrain>(tenantId);
        var response = await tenantGrain.Add(addTenant);
        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpPost()]
    public async Task<ActionResult<Dtos.AddTenant>> Post([FromBody]Dtos.Tenant tenant)
    {
        var tenantGrain = grainFactory.GetGrain<ITenantGrain>(tenant.Id);
        var response = await tenantGrain.Update(tenant);
        return response is null
            ? NotFound()
            : Ok(response);
    }
}
