﻿using FluentValidation;

namespace KavirTire.Shop.Application.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.title).NotEmpty();
        RuleFor(x => x.description).NotEmpty();
    }
}