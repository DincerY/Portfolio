using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Portfolio.Application.DTOs;
using Portfolio.Common.Response;
using ValidationException = Portfolio.CrossCuttingConcerns.Exceptions.ValidationException;

namespace Portfolio.API.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var value = context.ActionArguments.Values.First();

        if (value == null)
            return;
        Type validatorType;
        if (value is int)
        {
            validatorType = typeof(IValidator<>).MakeGenericType(typeof(EntityIdDTO));
        }
        else if (value is List<int>)
        {
            validatorType = typeof(IValidator<>).MakeGenericType(typeof(List<EntityIdDTO>));

        }
        else
        {
            validatorType = typeof(IValidator<>).MakeGenericType(value.GetType());
        }

        if (context.HttpContext.RequestServices.GetService(validatorType) is IValidator validator)
        {
            IValidationContext validationContext;
            if (value is int id)
            {
                validationContext = new ValidationContext<object>(new EntityIdDTO(){Id = id});
            }
            else if (value is List<int> list)
            {
                validationContext = new ValidationContext<object>(list.Select(id => new EntityIdDTO(){Id = id}).ToList());
            }
            else
            {
                validationContext = new ValidationContext<object>(value);
            }
            var res = validator.Validate(validationContext);

            if (!res.IsValid)
            {
                throw new ValidationException(res.Errors.Select(er => new ValidationError()
                {
                    Domain = er.PropertyName,
                    Message = er.ErrorMessage,
                    Reason = context.Controller.ToString()
                }).ToList());
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {

    }
}