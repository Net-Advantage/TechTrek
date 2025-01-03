namespace TechTrek.Tenant.Api;

public interface ITenantGrain : IGrainWithGuidKey
{
    Task<Nabs.Application.Response<Dtos.Tenant>> Get();
    Task<Nabs.Application.Response<Dtos.Tenant>> Add(Dtos.AddTenant addTenant);
    Task<Nabs.Application.Response<Dtos.Tenant>> Update(Dtos.Tenant request);
}
