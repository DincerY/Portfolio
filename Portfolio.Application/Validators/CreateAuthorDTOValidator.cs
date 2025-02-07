using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators;

public class CreateAuthorDTOValidator : AbstractValidator<CreateAuthorDTO>
{
    public CreateAuthorDTOValidator()
    {
        RuleFor(aut => aut.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(5,20).WithMessage("Name must be between 5 and 100 characters.");

        RuleFor(aut => aut.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .Length(5, 20).WithMessage("Surname must be at least 20 characters.");

    }
}