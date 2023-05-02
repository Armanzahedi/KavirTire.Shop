using Ardalis.GuardClauses;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Common.Services;
using KavirTire.Shop.Application.Common.Specifications;
using KavirTire.Shop.Application.Invoices.Specifications;
using KavirTire.Shop.Domain.IPGs;
using MediatR;

namespace KavirTire.Shop.Application.Invoices.Queries.GetInvoiceDetails;

public record GetInvoiceDetailsQuery(Guid InvoiceId) : IRequest<GetInvoiceDetailsQueryResult>;

public class GetInvoiceDetailsQueryHandler : IRequestHandler<GetInvoiceDetailsQuery, GetInvoiceDetailsQueryResult>
{
    private readonly IInvoiceRepository _invoiceRepo;
    private readonly IReadRepository<Ipg> _ipgRepo;
    private readonly IDateTimeProvider _dateTimeProvider;

    public GetInvoiceDetailsQueryHandler(IInvoiceRepository invoiceRepo,
        IReadRepository<Ipg> ipgRepo,
        IDateTimeProvider dateTimeProvider)
    {
        _invoiceRepo = invoiceRepo;
        _ipgRepo = ipgRepo;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<GetInvoiceDetailsQueryResult> Handle(GetInvoiceDetailsQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepo.FirstOrDefaultAsync(new InvoiceWithInvoiceItemsByIdSpec(request.InvoiceId), cancellationToken);
        Guard.Against.Null(invoice, null, "فاکتور مورد نظر پیدا نشد.");

        var ipgs = (await _ipgRepo.ListAsync(new IpgWithBankAccountsSpec(), cancellationToken))
            .Where(x => x.DisableFromHour == null ||
                         x.DisableFromMinute == null ||
                         x.DisableToHour == null ||
                         x.DisableToMinute == null ||
                         x.DisableRange.Includes(_dateTimeProvider.Now) == false)
            .ToList();
        
        return new GetInvoiceDetailsQueryResult
        {
            Invoice = invoice,
            Ipgs = ipgs
        };
    }
}