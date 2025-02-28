using FluentValidation;

namespace Portfolio.Application.Features.Categories.CreateCategory;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(dto => dto.Name)
            .Matches(@"^[a-zA-Z]+$").WithMessage("Name must contain only letters.")
            .NotEmpty().WithMessage("Name is required")
            .Length(5, 20).WithMessage("Name must be between 5 and 20 characters.");
        RuleFor(dto => dto.Description)
            .Matches(@"^[a-zA-Z]+$").WithMessage("Description must contain only letters.")
            .NotEmpty().WithMessage("Description is required")
            .Length(5, 100).WithMessage("Description must be between 5 and 100 characters.");
    }
}