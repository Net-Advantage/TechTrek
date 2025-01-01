namespace TechTrek.Tenant.Api;

public interface ITenantGrain : IGrainWithGuidKey
{
    Task<Dtos.Tenant> Get();
    Task<Dtos.Tenant> Update(Dtos.Tenant tenant);
}
