using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Payments.Services;
using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.IPGs;
using KavirTire.Shop.Infrastructure.Common;
using Microsoft.Extensions.Options;

namespace KavirTire.Shop.Infrastructure.Payment;

public class CreatePaymentService : ICreatePaymentService
{
    private readonly SequenceGenerator _sequenceGenerator;
    private readonly PaymentOptions _paymentOptions;
    private readonly IRepository<Domain.Payments.Payment> _paymentRepository;

    public CreatePaymentService(SequenceGenerator sequenceGenerator,
        IOptions<PaymentOptions> paymentOptions,
        IRepository<Domain.Payments.Payment> paymentRepository)
    {
        _sequenceGenerator = sequenceGenerator;
        _paymentRepository = paymentRepository;
        _paymentOptions = paymentOptions.Value;
    }
    public async Task<Domain.Payments.Payment> CreatePayment(Invoice invoice, Ipg ipg, Guid bankAccountId,
        CancellationToken cancellationToken = new())
    {
        var payment = new Domain.Payments.Payment
        {
            Amount = invoice.TotalCost,
            CustomerId = invoice.CustomerId,
            InvoiceId = invoice.Id,
            IpgId = ipg.Id,
            ResNo = (await _sequenceGenerator.GetNext("payment", _paymentOptions.ResNoStartFrom, cancellationToken)).ToString(),
            BankAccountId = bankAccountId,
            PostBankAccountId = ipg.PostBankAccountId,
        };
        payment.LongInfo("قبض پرداخت ایجاد شد.");
        await _paymentRepository.AddAsync(payment, cancellationToken);
        
        return payment;
    }
}