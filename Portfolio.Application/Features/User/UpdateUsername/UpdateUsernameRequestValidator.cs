using FluentValidation;

namespace Portfolio.Application.Features.User.UpdateUser;

public class UpdateUsernameRequestValidator : AbstractValidator<UpdateUsernameRequest>
{
    public UpdateUsernameRequestValidator()
    {
        RuleFor(r => r.NewUsername)
            .MinimumLength(6)
            .WithMessage("Username must be greater than 6 character")
            .NotEmpty()
            .WithMessage("Username is not be null")
            .NotNull();

    }
}