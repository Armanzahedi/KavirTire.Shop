using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.Payments.Entities;
using KavirTire.Shop.Domain.Payments.Enums;

namespace KavirTire.Shop.Domain.Payments;
public class Payment : EntityBase<Guid> , IAggregateRoot
{
    public Payment()
    {
        
    }

    public decimal Amount { get; set; }
    public Guid CustomerId { get; set; }
    public Guid InvoiceId { get; set; }
    public Guid IpgId { get; set; }
    public Guid BankAccountId { get; set; }
    public Guid? PostBankAccountId { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime? ProcessDate { get; set; }
    
    public string? PaymentIdentity { get; set; }
    public string? SystemTraceNo { get; set; }
    public long? RRN { get; set; }
    public string? RefNo { get; set; }
    public string ResNo { get; set; }
    public string? SecurePan { get; set; }

    private readonly List<PaymentLog> _paymentLogs = new();
    public IEnumerable<PaymentLog>  PaymentLogs =>  _paymentLogs.AsReadOnly();

    public void ProcessPayment(PaymentStatus status)
    {
        this.Status = status;
        this.ProcessDate = DateTime.Now;
    }
    public void LongInfo(string message)
    {
        _paymentLogs.Add(new PaymentLog
        {
            Type = PaymentLogType.Info,
            Message = message,
        });
    }
}