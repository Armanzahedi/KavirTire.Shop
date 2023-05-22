using Ardalis.GuardClauses;
using FluentValidation.Results;
using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Common.Exceptions;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Common.Services;
using KavirTire.Shop.Application.Common.Specifications;
using KavirTire.Shop.Application.Invoices.Specifications;
using KavirTire.Shop.Application.Products.Specifications;
using KavirTire.Shop.Domain.Customers;
using KavirTire.Shop.Domain.GeneralPolicy;
using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.Invoices.Enums;
using KavirTire.Shop.Domain.Locations;
using KavirTire.Shop.Domain.Products;
using KavirTire.Shop.Domain.VehicleTypes;
using MediatR;

namespace KavirTire.Shop.Application.Invoices.Commands.CreateInvoice;

public record CreateInvoiceCommand() : IRequest<Guid>
{
    public List<CreateInvoiceCommandProducts> Products { get; set; }
    public Guid? ExistingInvoiceId { get; set; }
}

public record CreateInvoiceCommandProducts(Guid ProductId, int Quantity);

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Guid>
{
    private readonly GeneralPolicyService _generalPolicyService;
    private readonly ICurrentUser _currentUser;
    private readonly IReadRepository<Customer> _customerRepo;
    private readonly IReadRepository<Location> _locationRepo;
    private readonly IInvoiceRepository _invoiceRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IReadRepository<VehicleType> _vehicleTypeRepo;
    private readonly IUnitOfWork _unitOfWork;


    public CreateInvoiceCommandHandler(
        ICurrentUser currentUser,
        IReadRepository<Customer> customerRepo,
        IReadRepository<Location> locationRepo,
        IInvoiceRepository invoiceRepo,
        IReadRepository<Product> productRepo,
        IReadRepository<VehicleType> vehicleTypeRepo,
        IUnitOfWork unitOfWork,
        GeneralPolicyService generalPolicyService)
    {
        _currentUser = currentUser;
        _customerRepo = customerRepo;
        _locationRepo = locationRepo;
        _invoiceRepo = invoiceRepo;
        _productRepo = productRepo;
        _vehicleTypeRepo = vehicleTypeRepo;
        _unitOfWork = unitOfWork;
        _generalPolicyService = generalPolicyService;
    }


    public async Task<Guid> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(_currentUser.UserId, "Current User Id");
        var generalPolicy = await _generalPolicyService.GetGeneralPolicy(cancellationToken);
        
        Guard.Against.Null(generalPolicy,null,"خطای سیستمی. قوانین سایت پیدا نشد.");
        Guard.Against.Null(generalPolicy.PriceListId, null,"خطای سیستمی. لیست قیمت پیدا نشد.");

        var customer = await _customerRepo.FirstOrDefaultAsync(new CustomerWithOrderHistoryVehicleByIdSpec(_currentUser.UserId.Value), cancellationToken);
        Guard.Against.Null(customer, null,"کاربر درخواست دهنده پیدا نشد.");
        Guard.Against.Null(customer.Vehicle?.VehicleTypeId, null,"Vehicle's VehicleType");

        var vehicleType = await _vehicleTypeRepo.GetByIdAsync(customer.Vehicle.VehicleTypeId.Value, cancellationToken);
        
        ValidateRequestWithGeneralPolicy(request, generalPolicy, customer.PreviousPurchaseCount());

        decimal postCost = await GetTirePostCostForCustomer(customer, cancellationToken);
        
        var products = await _productRepo.ListAsync(new ProductWithChildrenSpec(), cancellationToken);

        var invoice = new Invoice()
        {
            CustomerId = customer.Id,
            CustomerName = $"{customer.FirstName} {customer.LastName}",
            NationalId = customer.NationalId,
            PostalCode = customer.PostalCode,
            PostalAddress = customer.PostalAddress,
            MobilePhone = customer.MobilePhone,
            Vehicle = vehicleType!.Name,
            PriceListId = generalPolicy.PriceListId.Value,
            RegistrationPlate = customer.Vehicle.RegistrationPlate,
            InvoiceStatus = InvoiceStatus.Draft,
            CreateDate = DateTime.Now
        };
        foreach (var item in request.Products)
        {
            var product = products.FirstOrDefault(x => x.Id == item.ProductId);
            Guard.Against.Null(product, null,"محصول درخواستی پیدا نشد.");
            
            invoice.AddInvoiceItem(product!,item.Quantity,generalPolicy.PriceListId!.Value,postCost);   
        }

        if (request.ExistingInvoiceId != null && await _invoiceRepo.AnyAsync(new InvoiceByIdSpec(request.ExistingInvoiceId.Value), cancellationToken))
        {
            _unitOfWork.BeginTransaction();
            invoice.Id = request.ExistingInvoiceId.Value;
            await _invoiceRepo.DeleteInvoiceItemsAsync(invoice.Id, cancellationToken);
            await _invoiceRepo.UpdateAsync(invoice, cancellationToken);
            await _unitOfWork.SaveAndCommitAsync(cancellationToken);
        }
        else
        {
            await _invoiceRepo.AddAsync(invoice, cancellationToken);
        }

        return invoice.Id;
    }

    private static void ValidateRequestWithGeneralPolicy(CreateInvoiceCommand request, GeneralPolicy generalPolicy,
        int previousCustomerPurchaseCount)
    {
        if (generalPolicy.ApplyNumberOfPurchaseItems)
            Guard.Against.InvalidInput(request.Products.Count, "Product Count",
                x => x <= generalPolicy.NumberOfPurchaseItems,
                $"حداکثر تعداد محصولات سبد خرید {generalPolicy.NumberOfPurchaseItems} عدد میباشد");

        if (generalPolicy.ApplyMaximumNumberOfPurchases)
        {
            foreach (var item in request.Products)
            {
                Guard.Against.InvalidInput(item.Quantity, $"Product Quantity with Id => {item.Quantity}",
                    x => x <= generalPolicy.MaximumNumberOfPurchases,
                    $"حداکثر تعداد اقلام یک محصول {generalPolicy.MaximumNumberOfPurchases} عدد میباشد");
            }
        }

        if (generalPolicy.ApplyPurchaseInterval)
        {
            Guard.Against.InvalidInput(request.Products.Sum(x => x.Quantity),
                $"Total Product Quantity",
                x => x <= generalPolicy.MaximumNumberOfPurchases - previousCustomerPurchaseCount,
                $"حداکثر تعداد محصولات سبد خرید شما {generalPolicy.MaximumNumberOfPurchases - previousCustomerPurchaseCount} عدد میباشد");
        }
    }

    private async Task<decimal> GetTirePostCostForCustomer(Customer customer, CancellationToken cancellationToken)
    {
        decimal tirePostCost = 0;
        if (customer.ProvinceId != null)
        {
            var location =
                await _locationRepo.FirstOrDefaultAsync(
                    new LocationWithPostCostCategoryByIdSpec(customer.ProvinceId.Value),
                    cancellationToken);
            tirePostCost = location?.GetPostCost() ?? 0;
        }

        return tirePostCost;
    }
}