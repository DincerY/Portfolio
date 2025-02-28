using MediatR;

namespace Portfolio.Application.Behaviors;

public class MappingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest,TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();


        return response;
    }
}