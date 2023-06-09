﻿using FluentValidation;

namespace KavirTire.Shop.Application.Projects.Commands.DeleteProject;

public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator()
    {
        RuleFor(x => x.projectId).NotEmpty();
    }
}