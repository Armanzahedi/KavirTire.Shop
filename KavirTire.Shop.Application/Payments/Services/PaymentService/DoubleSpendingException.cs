namespace KavirTire.Shop.Application.Payments.Services.PaymentService;

public class DoubleSpendingException : Exception
{
    public DoubleSpendingException()
        : base(" تراکنش تکراری ")
    {
    }

    public DoubleSpendingException(string message)
        : base(" تراکنش تکراری " + message)
    {
    }

    public DoubleSpendingException(string message, Exception innerException)
        : base(" تراکنش تکراری " + message, innerException)
    {
    }
}