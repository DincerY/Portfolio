﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Portfolio.Common.Response;
using ValidationException = Portfolio.CrossCuttingConcerns.Exceptions.ValidationException;

namespace Portfolio.API.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.Count <= 0)
        {
            return;
        }
        var value = context.ActionArguments.Values.First();

        if (value == null)
            return;

        Type validatorType = typeof(IValidator<>).MakeGenericType(value.GetType());


        if (context.HttpContext.RequestServices.GetService(validatorType) is IValidator validator)
        {
            IValidationContext validationContext = new ValidationContext<object>(value);
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