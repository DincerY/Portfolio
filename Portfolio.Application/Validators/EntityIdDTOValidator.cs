﻿using System.Data;
using FluentValidation;
using Portfolio.Application.DTOs;

namespace Portfolio.Application.Validators;

public class EntityIdDTOValidator : AbstractValidator<EntityIdDTO>
{
    public EntityIdDTOValidator()
    {
        RuleFor(ent => ent.Id)
            .GreaterThan(0)
            .WithMessage("ID must be a positive integer.")
            .NotEmpty()
            .WithMessage("Id must not be null");
    }
}

/*public class IdValidator : AbstractValidator<int>
{
    public IdValidator()
    {
        RuleFor(ent => ent)
            .GreaterThan(0)
            .WithMessage("ID must be a positive integer.")
            .NotEmpty()
            .WithMessage("Id must not be null");
    }
}*/