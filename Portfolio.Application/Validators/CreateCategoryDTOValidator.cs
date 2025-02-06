using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators;

public class CreateCategoryDTOValidator : AbstractValidator<CreateCategoryDTO>
{
    public CreateCategoryDTOValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(5, 20).WithMessage("Name must be between 5 and 20 characters.");
        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required")
            .Length(5, 100).WithMessage("Description must be between 5 and 100 characters.");
    }
}