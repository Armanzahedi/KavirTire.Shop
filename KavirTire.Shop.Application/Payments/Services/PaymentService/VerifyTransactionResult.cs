namespace KavirTire.Shop.Application.Payments.Services.PaymentService;

public class VerifyTransactionResult
{
    public bool IsSuccessful { get; set; }
    public string? Massage { get; set; }
    public string? ProviderMessage { get; set; }
    public int? ProviderState { get; set; }
    public long? RRN { get; set; }
    public string? RefNum { get; set; }
    public string? ResNo { get; set; }
    public string? SecurePan { get; set; }
    public string? PaymentIdentity { get; set; }
    public decimal? Amount { get; set; }
    public string? SystemTraceNo { get; set; }
}