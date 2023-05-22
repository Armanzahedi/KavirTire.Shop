using Ardalis.GuardClauses;
using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Common.Services;
using KavirTire.Shop.Application.Common.Specifications;
using KavirTire.Shop.Application.InventoryItems.Services;
using KavirTire.Shop.Application.Payments.Services;
using KavirTire.Shop.Application.Payments.Services.PaymentGateway;
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
    private readonly IRepository<Payment> _paymentRepo;
    private readonly IReadRepository<Ipg> _ipgRepo;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentGatewayFactory _paymentGatewayFactory;
    private readonly InventoryItemReservationService _inventoryItemReservationService;
    private readonly ICreatePaymentService _createPaymentService;

    private readonly IDistributedLock _distributedLock;


    public CreatePaymentCommandHandler(
        ICurrentUser currentUser,
        IInvoiceRepository invoiceRepo,
        IRepository<Payment> paymentRepo,
        IReadRepository<Ipg> ipgRepo,
        IReadRepository<InventoryItem> inventoryItemReadRepo,
        IUnitOfWork unitOfWork,
        IPaymentGatewayFactory paymentGatewayFactory,
        InventoryItemReservationService inventoryItemReservationService,
        GeneralPolicyService generalPolicyService,
        IDistributedLock distributedLock, ICreatePaymentService createPaymentService)
    {
        _currentUser = currentUser;
        _invoiceRepo = invoiceRepo;
        _paymentRepo = paymentRepo;
        _ipgRepo = ipgRepo;
        _inventoryItemReadRepo = inventoryItemReadRepo;
        _unitOfWork = unitOfWork;
        _paymentGatewayFactory = paymentGatewayFactory;
        _inventoryItemReservationService = inventoryItemReservationService;
        _generalPolicyService = generalPolicyService;
        _distributedLock = distributedLock;
        _createPaymentService = createPaymentService;
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

            var paymentGateway = _paymentGatewayFactory.Create(ipg);

            _unitOfWork.BeginTransaction();

            Payment payment;

            var existingPayment =
                await _paymentRepo.FirstOrDefaultAsync(new PaymentByInvoiceId(invoice.Id), cancellationToken);

            if(existingPayment != null)
                payment = existingPayment;
            else
            {
                payment = await _createPaymentService.CreatePayment(invoice, ipg, request.BankAccountId, cancellationToken);
                
                invoice.SetExpiration(generalPolicy);
                await _invoiceRepo.UpdateAsync(invoice, cancellationToken);
                
                foreach (var item in invoice.InvoiceItems)
                {
                    var inventoryItemId = await _inventoryItemReadRepo
                        .FirstOrDefaultAsync(new InventoryItemIdByProductIdSpec(item.ProductId), cancellationToken);

                    await _inventoryItemReservationService.Reserve(inventoryItemId, item.Quantity, cancellationToken);
                }
            }


            var gatewayUrl = await paymentGateway
                .GetGatewayUrl(invoice,
                    payment,
                    ipg.BankAccounts.FirstOrDefault(x => x.Id == request.BankAccountId)!,
                    ipg.BankAccounts.FirstOrDefault(x => x.IsPost == true),
                    invoice.MobilePhone);

            await _unitOfWork.SaveAndCommitAsync(cancellationToken);

            return gatewayUrl;
        }, cancellationToken);
        

        return new CreatePaymentCommandResponse(url!);
    }
}