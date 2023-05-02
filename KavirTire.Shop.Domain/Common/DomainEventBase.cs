using MediatR;

namespace KavirTire.Shop.Domain.Common;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
