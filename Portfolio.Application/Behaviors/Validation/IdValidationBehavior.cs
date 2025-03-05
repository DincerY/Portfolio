using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using Portfolio.Common.Response;
using Portfolio.CrossCuttingConcerns.Exceptions;

namespace Portfolio.Application.Behaviors.Validation;

public class IdValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var typeRequest = typeof(TRequest);
        var property = typeRequest.GetProperty("Id");
        var listProperty = typeRequest.GetProperty("Ids");
        if (property != null)
        {
            var value = (int)property.GetValue(request, null);
            if (value <= 0)
            {
                throw new ValidationException(new List<ValidationError>()
                {
                    new()
                    {
                        Property = property.Name,
                        Errors = new []
                        {
                            "Id must be greater than zero."
                        }
                    }
                });

            }
        }

        if (listProperty != null)
        {
            var values = (List<int>)listProperty.GetValue(request, null);
            foreach (var value in values)
            {
                if (value <= 0)
                {
                    throw new ValidationException(new List<ValidationError>()
                    {
                        new()
                        {
                            Property = listProperty.Name,
                            Errors = new []
                            {
                                "Ids must be greater than zero."
                            }
                        }
                    });
                }
            }

        }

        var response = await next();
        return response;
    }
}