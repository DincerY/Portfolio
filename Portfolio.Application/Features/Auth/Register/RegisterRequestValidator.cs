using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Portfolio.Application.Features.Auth.Register;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(r => r.Email)
            .EmailAddress()
            .WithMessage("Email address is not correct form")
            .NotEmpty()
            .WithMessage("Email address is not be empty")
            .NotNull();
        RuleFor(r => r.Username)
            .MinimumLength(6)
            .WithMessage("Username must be greater than 6 character")
            .NotEmpty()
            .WithMessage("Username is not be null")
            .NotNull();
        RuleFor(r => r.Password)
            .MinimumLength(6)
            .WithMessage("Password must be greater than 6 character")
            .NotEmpty()
            .WithMessage("Name is not be null")
            .NotNull();
    }
}