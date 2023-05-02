using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.IPGs;
using KavirTire.Shop.Domain.IPGs.Entities;
using KavirTire.Shop.Domain.Payments;

namespace KavirTire.Shop.Application.Payments.Services.PaymentService;

public abstract class PaymentService
{
    protected readonly Ipg _ipg;
    public PaymentService(Ipg ipg)
    {
        _ipg = ipg;
    }

    public abstract Task<string> GetGatewayUrl(Invoice invoice, Payment payment,BankAccount bankAccount,BankAccount? postBankAccount = null,string? customerMobileNumber = null);
    public abstract Task<VerifyTransactionResult> VerifyTransaction(Dictionary<string,string?> bankResponse);
    public abstract Task ReverseTransaction(Dictionary<string, string?> bankResponse);
}