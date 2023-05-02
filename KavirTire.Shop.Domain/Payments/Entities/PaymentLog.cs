using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Payments.Enums;

namespace KavirTire.Shop.Domain.Payments.Entities;

public class PaymentLog : EntityBase<Guid>
{
    public PaymentLog()
    {
        CreateDate = DateTime.Now;
    }
    public Guid PaymentId { get; set; }

    public PaymentLogType Type { get; set; }
    public DateTime CreateDate { get; set; }
    public string? Message { get; set; }
    public string? ErrorCode { get; set; }
    public string? Details { get; set; }
    
}