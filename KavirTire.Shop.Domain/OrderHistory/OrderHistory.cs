using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;

namespace KavirTire.Shop.Domain.OrderHistory;

public class OrderHistory : EntityBase<Guid>, IAggregateRoot
{
    public Guid CustomerId { get; set; }
    public Guid OrderId { get; set; }
    public int TotalQuantity { get; set; }
    public DateTime? RegistrationDate { get; set; }
}