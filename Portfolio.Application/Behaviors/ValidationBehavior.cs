using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Portfolio.Common.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ValidationException = Portfolio.CrossCuttingConcerns.Exceptions.ValidationException;

namespace Portfolio.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest,TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;


    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<object> context = new ValidationContext<object>(request);
        var errors = _validators
            .Select(val => val.Validate(context))
            .SelectMany(res => res.Errors)
            .Where(fail => fail != null)
            .GroupBy(keySelector: p => p.PropertyName,
                resultSelector: (propertyName, errors) => new ValidationError()
                {
                    Property = propertyName,
                    Errors = errors.Select(e => e.ErrorMessage)
                }).ToList();
        if (errors.Any())
        {
            throw new ValidationException(errors);
        }

        TResponse response = await next();
        return response;
    }
}