using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Common.Services;
using KavirTire.Shop.Application.Common.Specifications;
using KavirTire.Shop.Domain.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace KavirTire.Shop.Presentation.Filters;

public class CustomerValidForPurchaseFilterAttribute: Attribute, IAsyncActionFilter
{
    private readonly ICurrentUser _currentUser;
    private readonly IReadRepository<Customer> _customerRepo;
    private readonly KavirTireOptions _kavirTireOptions;

    public CustomerValidForPurchaseFilterAttribute(ICurrentUser currentUser,
        IOptions<KavirTireOptions> kavirTireOptions,
        IReadRepository<Customer> customerRepo)
    {
        _currentUser = currentUser;
        _customerRepo = customerRepo;
        _kavirTireOptions = kavirTireOptions.Value;
    }


    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var customerIsApprovedForPurchase = _currentUser.UserId == null || await _customerRepo.FirstOrDefaultAsync( new CustomerIsApprovedForPurchaseSpec(_currentUser.UserId)) == false;
        
        if (customerIsApprovedForPurchase)
            context.Result = new RedirectResult(_kavirTireOptions.PortalAddress);
        
        if (context.Result == null)
            await next();    
    }
}