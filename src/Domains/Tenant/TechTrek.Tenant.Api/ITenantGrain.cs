using Ardalis.Result;

namespace TechTrek.Tenant.Api;

public interface ITenantGrain : IGrainWithGuidKey
{
    Task<Result<Dtos.Tenant>> Get();
    Task<Result<Dtos.Tenant>> Add(Dtos.AddTenant addTenant);
    Task<Result<Dtos.Tenant>> Update(Dtos.Tenant request);
}
