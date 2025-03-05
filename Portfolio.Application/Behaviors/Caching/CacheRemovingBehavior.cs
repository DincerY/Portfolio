using MediatR;

namespace Portfolio.Application.Behaviors.Caching;

public class CacheRemovingBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse>  where TRequest : ICacheRemoverRequest
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}