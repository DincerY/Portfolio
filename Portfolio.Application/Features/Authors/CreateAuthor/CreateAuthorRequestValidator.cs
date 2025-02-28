using FluentValidation;

namespace Portfolio.Application.Features.Authors.CreateAuthor;

public class CreateAuthorRequestValidator : AbstractValidator<CreateAuthorRequest>
{
    public CreateAuthorRequestValidator()
    {
        RuleFor(aut => aut.Name)
            .Matches(@"^[a-zA-Z]+$").WithMessage("Name must contain only letters.")
            .NotEmpty().WithMessage("Name is required.")
            .Length(5, 20).WithMessage("Name must be between 5 and 100 characters.");

        RuleFor(aut => aut.Surname)
            .Matches(@"^[a-zA-Z]+$").WithMessage("Surname must contain only letters.")
            .NotEmpty().WithMessage("Surname is required.")
            .Length(5, 20).WithMessage("Surname must be at least 20 characters.");
    }
}