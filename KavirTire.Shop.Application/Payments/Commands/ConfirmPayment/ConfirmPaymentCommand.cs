using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Common.Specifications;
using KavirTire.Shop.Application.InventoryItems.Services;
using KavirTire.Shop.Application.Payments.Services.PaymentService;
using KavirTire.Shop.Application.Payments.Specifications;
using KavirTire.Shop.Domain.InventoryItems;
using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.IPGs;
using KavirTire.Shop.Domain.Payments;
using KavirTire.Shop.Domain.Payments.Enums;
using MediatR;

namespace KavirTire.Shop.Application.Payments.Commands.ConfirmPayment;

public record ConfirmPaymentCommand(Guid IpgId, Guid PaymentId, Guid InvoiceId,
    Dictionary<string, string?> BankResponse) : IRequest<ConfirmPaymentCommandResult>;


public class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand, ConfirmPaymentCommandResult?>
{
    private readonly IReadRepository<Ipg> _ipgRepo;
    private readonly IPaymentServiceFactory _paymentServiceFactory;
    private readonly IInvoiceRepository _invoiceReadRepo;
    private readonly IRepository<Invoice> _invoiceRepo;
    private readonly IRepository<Payment> _paymentRepo;
    private readonly IDistributedLock _distributedLock;

    private readonly IReadRepository<InventoryItem> _inventoryItemReadRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly InventoryItemReservationService _inventoryItemReservationService;


    public ConfirmPaymentCommandHandler(IPaymentServiceFactory paymentServiceFactory,
        IReadRepository<Ipg> ipgRepo,
        IInvoiceRepository invoiceReadRepo,
        IRepository<Invoice> invoiceRepo,
        IDistributedLock distributedLock,
        IReadRepository<InventoryItem> inventoryItemReadRepo,
        IUnitOfWork unitOfWork,
        InventoryItemReservationService inventoryItemReservationService,
        IRepository<Payment> paymentRepo)
    {
        _paymentServiceFactory = paymentServiceFactory;
        _ipgRepo = ipgRepo;
        _invoiceReadRepo = invoiceReadRepo;
        _invoiceRepo = invoiceRepo;
        _distributedLock = distributedLock;
        _inventoryItemReadRepo = inventoryItemReadRepo;
        _unitOfWork = unitOfWork;
        _inventoryItemReservationService = inventoryItemReservationService;
        _paymentRepo = paymentRepo;
    }

    public async Task<ConfirmPaymentCommandResult?> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
    {
        return await _distributedLock.Lock($"Invoice-{request.InvoiceId}", async () =>
        {
            var invoice = await _invoiceReadRepo.FirstOrDefaultAsync(new InvoiceWithInvoiceItemsByIdSpec(request.InvoiceId),
                cancellationToken);

            var ipg = await _ipgRepo.GetByIdAsync(request.IpgId, cancellationToken);
            var payment = await _paymentRepo.GetByIdAsync(request.PaymentId, cancellationToken);
            var paymentService = _paymentServiceFactory.Create(ipg!);

            _unitOfWork.BeginTransaction();


            if (invoice!.IsExpired)
            {
                payment!.LongInfo("پیش فاکتور منقضی شده و وجه برگشت داده شد.");
                payment.ProcessPayment(PaymentStatus.Reversed);

                await paymentService.ReverseTransaction(request.BankResponse);
                return new ConfirmPaymentCommandResult(false, "پیش فاکتور شما منقضی شده در صورت پرداخت وجه مبلغ پرداخت شده تا 72 ساعت آینده به حساب شما واریز میشود.", null);
            }

            VerifyTransactionResult? verification;
            try
            {
                verification = await paymentService.VerifyTransaction(request.BankResponse);
            }
            catch (DoubleSpendingException e)
            {
                return new ConfirmPaymentCommandResult(false, e.Message, null);
            }

            invoice.Close();
            payment!.LongInfo(verification.IsSuccessful ? "پرداخت با موفقیت انجام شد." : "پرداخت با خطا مواجه شد.");
            payment!.ProcessPayment(verification.IsSuccessful ? PaymentStatus.Confirmed : PaymentStatus.Failed);
            payment.PaymentIdentity = verification.PaymentIdentity;
            payment.SystemTraceNo = verification.SystemTraceNo;
            payment.RRN = verification.RRN;
            payment.RefNo = verification.RefNum;
            payment.SecurePan = verification.SecurePan;

            foreach (var item in invoice.InvoiceItems)
            {
                var inventoryItemId = await _inventoryItemReadRepo
                    .FirstOrDefaultAsync(new InventoryItemIdByProductIdSpec(item.ProductId), cancellationToken);

                if (verification.IsSuccessful)
                    await _inventoryItemReservationService.RemoveFromInventory(inventoryItemId, item.Quantity, cancellationToken);
                else
                    await _inventoryItemReservationService.ReleaseFromReserve(inventoryItemId, item.Quantity, cancellationToken);
            }

            await _invoiceRepo.UpdateAsync(invoice, cancellationToken);
            await _unitOfWork.SaveAndCommitAsync(cancellationToken);

            return new ConfirmPaymentCommandResult(verification.IsSuccessful, verification.Massage, verification.PaymentIdentity);
        }, cancellationToken);
    }
}