namespace KavirTire.Shop.Presentation.Models.Payment;

public class PaymentConfirmationResult
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
    public string? TraceNo { get; set; }
}