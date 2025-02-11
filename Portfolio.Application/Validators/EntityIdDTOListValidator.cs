using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators;

public class EntityIdDTOListValidator : AbstractValidator<List<EntityIdDTO>>
{
    public EntityIdDTOListValidator()
    {
        RuleForEach(ent => ent).SetValidator(new EntityIdDTOValidator());
    }
}