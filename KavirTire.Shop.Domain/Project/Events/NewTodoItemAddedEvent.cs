using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Project.Entities;

namespace KavirTire.Shop.Domain.Project.Events;

public class NewTodoItemAddedEvent: DomainEventBase
{
    private TodoItem NewItem { get; }
    private Project Project { get; }

    public NewTodoItemAddedEvent(Project project,
        TodoItem newItem)
    {
        Project = project;
        NewItem = newItem;
    }
}