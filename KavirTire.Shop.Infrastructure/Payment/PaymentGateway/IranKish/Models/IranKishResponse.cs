namespace KavirTire.Shop.Infrastructure.Payment.PaymentGateway.IranKish.Models;

public class IranKishResponse
{
    public string responseCode { get; set; }
    public string token { get; set; }
    public string acceptorId { get; set; }
    public string amount { get; set; }
    public string paymentId { get; set; }
    public string requestId { get; set; }
    public string retrievalReferenceNumber { get; set; }
    public string systemTraceAuditNumber { get; set; }
}