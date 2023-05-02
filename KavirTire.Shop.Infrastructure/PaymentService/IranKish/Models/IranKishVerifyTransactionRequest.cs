using System.Runtime.Serialization;

namespace KavirTire.Shop.Infrastructure.PaymentService.IranKish.Models;

public class IranKishVerifyTransactionRequest
{
    [DataMember]
    public string terminalId { get; set; }

    [DataMember]
    public string retrievalReferenceNumber { get; set; }

    [DataMember]
    public string systemTraceAuditNumber { get; set; }

    [DataMember]
    public string tokenIdentity { get; set; }
}