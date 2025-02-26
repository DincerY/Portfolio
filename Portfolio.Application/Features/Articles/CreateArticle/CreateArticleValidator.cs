using FluentValidation;

namespace Portfolio.Application.Features.Articles.CreateArticle;

public class CreateArticleValidator : AbstractValidator<CreateArticleRequest>
{
    public CreateArticleValidator()
    {
        RuleFor(art => art.Title)
            .Matches(@"^[a-zA-Z]+$").WithMessage("Title must contain only letters.")
            .NotEmpty().WithMessage("Title is required.")
            .Length(5, 100).WithMessage("Title must be between 5 and 100 characters.");

        RuleFor(art => art.Name)
            .Matches(@"^[a-zA-Z]+$").WithMessage("Name must contain only letters.")
            .NotEmpty().WithMessage("Name is required.")
            .Length(5, 20).WithMessage("Name must be between 5 and 100 characters.");

        RuleFor(art => art.Content)
            .Matches(@"^[a-zA-Z]+$").WithMessage("Content must contain only letters.")
            .NotEmpty().WithMessage("Content is required.")
            .Length(5, 100).WithMessage("Content must be between 5 and 100 characters.");

        RuleFor(art => art.Authors)
            .NotEmpty().WithMessage("Article must have minimum one author.");

        RuleFor(art => art.Categories)
            .NotEmpty().WithMessage("Article must have minimum one category.");
    }
}