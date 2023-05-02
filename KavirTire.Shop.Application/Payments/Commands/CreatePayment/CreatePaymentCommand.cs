using Ardalis.GuardClauses;
using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Common.Services;
using KavirTire.Shop.Application.Common.Specifications;
using KavirTire.Shop.Application.InventoryItems.Services;
using KavirTire.Shop.Application.Payments.Services.PaymentService;
using KavirTire.Shop.Application.Payments.Specifications;
using KavirTire.Shop.Domain.InventoryItems;
using KavirTire.Shop.Domain.IPGs;
using KavirTire.Shop.Domain.Payments;
using MediatR;

namespace KavirTire.Shop.Application.Payments.Commands.CreatePayment;

public record CreatePaymentCommand(Guid InvoiceId, Guid BankAccountId) : IRequest<CreatePaymentCommandResponse>;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentCommandResponse>
{
    private readonly GeneralPolicyService _generalPolicyService;
    private readonly IInvoiceRepository _invoiceRepo;
    private readonly IReadRepository<InventoryItem> _inventoryItemReadRepo;
    private readonly IRepository<Domain.Payments.Payment> _paymentRepo;
    private readonly IReadRepository<Ipg> _ipgRepo;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentServiceFactory _paymentServiceFactory;
    private readonly InventoryItemReservationService _inventoryItemReservationService;
    private readonly ISequenceGenerator _sequenceGenerator;

    private readonly IDistributedLock _distributedLock;


    public CreatePaymentCommandHandler(
        ICurrentUser currentUser,
        IInvoiceRepository invoiceRepo,
        IRepository<Domain.Payments.Payment> paymentRepo,
        IReadRepository<Ipg> ipgRepo,
        IReadRepository<InventoryItem> inventoryItemReadRepo,
        IUnitOfWork unitOfWork,
        IPaymentServiceFactory paymentServiceFactory,
        InventoryItemReservationService inventoryItemReservationService,
        GeneralPolicyService generalPolicyService,
        ISequenceGenerator sequenceGenerator,
        IDistributedLock distributedLock)
    {
        _currentUser = currentUser;
        _invoiceRepo = invoiceRepo;
        _paymentRepo = paymentRepo;
        _ipgRepo = ipgRepo;
        _inventoryItemReadRepo = inventoryItemReadRepo;
        _unitOfWork = unitOfWork;
        _paymentServiceFactory = paymentServiceFactory;
        _inventoryItemReservationService = inventoryItemReservationService;
        _generalPolicyService = generalPolicyService;
        _sequenceGenerator = sequenceGenerator;
        _distributedLock = distributedLock;
    }

    public async Task<CreatePaymentCommandResponse> Handle(CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        string? url = await _distributedLock.Lock($"Invoice-{request.InvoiceId}", async () => 
        {
            Guard.Against.Null(_currentUser.UserId, "Current User Id");

            var generalPolicy = await _generalPolicyService.GetGeneralPolicy(cancellationToken);

            Guard.Against.Null(generalPolicy, null, "خطای سیستمی. قوانین سایت پیدا نشد.");
            Guard.Against.Null(generalPolicy.PriceListId, null, "خطای سیستمی. لیست قیمت پیدا نشد.");

            var invoice = await _invoiceRepo.FirstOrDefaultAsync(new InvoiceWithInvoiceItemsByIdSpec(request.InvoiceId),
                cancellationToken);
            Guard.Against.Null(invoice, null, "فاکتور پیدا نشد.");

            var ipg = await _ipgRepo.FirstOrDefaultAsync(new IpgByBankAccountIdSpec(request.BankAccountId),
                cancellationToken);
            Guard.Against.Null(ipg, null, "درگاه نا معتبر.");
            Guard.Against.Null(ipg.BankAccounts, null, "حساب نا معتبر.");

            var paymentGateway = _paymentServiceFactory.Create(ipg);

            _unitOfWork.BeginTransaction();



            Payment payment;

            var existingPayment =
                await _paymentRepo.FirstOrDefaultAsync(new PaymentByInvoiceId(invoice.Id), cancellationToken);

            //if(existingPayment != null)
            //    payment = existingPayment;
            //else
            //{
            //    var resNo = (await _sequenceGenerator.GetNext(cancellationToken)).ToString();
            //    payment = new Payment(resNo)
            //    {
            //        Amount = invoice.TotalCost,
            //        CustomerId = invoice.CustomerId,
            //        InvoiceId = invoice.Id,
            //        IpgId = ipg.Id,
            //        BankAccountId = request.BankAccountId,
            //        PostBankAccountId = ipg.PostBankAccountId,
            //    };

            //    invoice.SetExpiration(generalPolicy);
            //    foreach (var item in invoice.InvoiceItems)
            //    {
            //        var inventoryItemId = await _inventoryItemReadRepo
            //            .FirstOrDefaultAsync(new InventoryItemIdByProductIdSpec(item.ProductId), cancellationToken);

            //        await _inventoryItemReservationService.Reserve(inventoryItemId, item.Quantity, cancellationToken);
            //    }

            //    payment.LongInfo("قبض پرداخت ایجاد شد.");
            //    await _paymentRepo.AddAsync(payment, cancellationToken);
            //}

            var resNo = (await _sequenceGenerator.GetNext(cancellationToken)).ToString();
            payment = new Payment(resNo)
            {
                Amount = invoice.TotalCost,
                CustomerId = invoice.CustomerId,
                InvoiceId = invoice.Id,
                IpgId = ipg.Id,
                BankAccountId = request.BankAccountId,
                PostBankAccountId = ipg.PostBankAccountId,
            };

            invoice.SetExpiration(generalPolicy);
            foreach (var item in invoice.InvoiceItems)
            {
                var inventoryItemId = await _inventoryItemReadRepo
                    .FirstOrDefaultAsync(new InventoryItemIdByProductIdSpec(item.ProductId), cancellationToken);

                await _inventoryItemReservationService.Reserve(inventoryItemId, item.Quantity, cancellationToken);
            }

            payment.LongInfo("قبض پرداخت ایجاد شد.");
            await _paymentRepo.AddAsync(payment, cancellationToken);

            await _unitOfWork.SaveAndCommitAsync(cancellationToken);

            //var gatewayUrl = await paymentGateway
            //    .GetGatewayUrl(invoice,
            //        payment,
            //        ipg.BankAccounts.FirstOrDefault(x => x.Id == request.BankAccountId)!,
            //        ipg.BankAccounts.FirstOrDefault(x => x.IsPost == true),
            //        invoice.MobilePhone);

            return Guid.NewGuid().ToString();
        }, cancellationToken);
        

        return new CreatePaymentCommandResponse(url!);
    }
}