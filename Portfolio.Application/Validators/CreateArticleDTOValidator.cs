using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators;

public class CreateArticleDTOValidator : AbstractValidator<CreateArticleDTO>
{
    public CreateArticleDTOValidator()
    {
        RuleFor(art => art.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(5, 100).WithMessage("Title must be between 5 and 100 characters.");

        RuleFor(art => art.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(5, 20).WithMessage("Name must be between 5 and 100 characters.");

        RuleFor(art => art.Content)
            .NotEmpty().WithMessage("Content is required.")
            .Length(5, 100).WithMessage("Content must be between 5 and 100 characters.");

        RuleFor(art => art.Authors)
            .NotEmpty().WithMessage("Article must have minimum one author.");

        RuleFor(art => art.Categories)
            .NotEmpty().WithMessage("Article must have minimum one category.");

    }
}