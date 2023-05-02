using Ardalis.Specification;
using KavirTire.Shop.Domain.Payments;

namespace KavirTire.Shop.Application.Payments.Specifications;

public class PaymentByRefNoSpec : Specification<Payment>
{
    public PaymentByRefNoSpec(string refNo)
    {
        Query.Where(x => x.RefNo == refNo);
    }
}