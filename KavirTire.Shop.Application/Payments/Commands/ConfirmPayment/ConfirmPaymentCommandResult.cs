namespace KavirTire.Shop.Application.Payments.Commands.ConfirmPayment;

public record ConfirmPaymentCommandResult(bool IsSuccessful, string? Message, string? TraceNo);

