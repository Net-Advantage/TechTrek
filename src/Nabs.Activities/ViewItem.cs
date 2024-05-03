using MediatR;

namespace Nabs.Activities;

public class ViewItem<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class, new()
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var result = new TResponse();
        return await Task.FromResult(result);
    }
}