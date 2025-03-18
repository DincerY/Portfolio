using FluentValidation;

namespace Portfolio.Application.Features.User.UpdateUser;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(r => r.NewUsername)
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