
namespace Nabs.TechTrek.TenantActivities.ListTenants;

public sealed class  ListTenantsRequest : IRequest<ListTenantsResponse>
{
    
}

public sealed class ListTenantsResponse
{

}

public sealed class ListTenantsHandler : IRequestHandler<ListTenantsRequest, ListTenantsResponse>
{
    public ListTenantsHandler()
    {
        
    }

    public Task<ListTenantsResponse> Handle(ListTenantsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
