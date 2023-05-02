﻿using Ardalis.GuardClauses;
using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Attributes;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.Project.Entities;
using KavirTire.Shop.Domain.Project.Enums;
using KavirTire.Shop.Domain.Project.Events;

namespace KavirTire.Shop.Domain.Project;

[Auditable]
public class Project : EntityBase<Guid>, IAggregateRoot, ISoftDeletableEntity
{
    private Project()  {  }
    private string Title { get;}
    public string? Description { get; set; }
    
    private readonly List<TodoItem> _items = new();
    public IEnumerable<TodoItem> Items => _items.AsReadOnly();

    public Project(string title,string description)
    {
        Guard.Against.NullOrEmpty(title, nameof(Title));
        Guard.Against.NullOrEmpty(description, nameof(Description));
  
        this.Title = title;
        this.Description = description;
    }

    
    public void AddTodo(string title,string? description)
    {
        var item = new TodoItem(title, description);
        _items.Add(item);
        this.RegisterDomainEvent(new NewTodoItemAddedEvent(this,item));
    }

    public ProjectStatus Status => _items.All(i => i.Done) ? ProjectStatus.Done : ProjectStatus.Created;

    public bool IsDeleted { get; set; }
}