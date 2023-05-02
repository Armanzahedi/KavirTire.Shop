using FluentValidation;

namespace KavirTire.Shop.Application.Projects.Commands.AddTodo;

public class AddTodoCommandValidator : AbstractValidator<AddTodoCommand>
{
    public AddTodoCommandValidator()
    {
        RuleFor(x => x.todoTile).NotEmpty();
    }
}