using Ardalis.GuardClauses;
using KavirTire.Shop.Application.Cart.Queries.GetCartInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KavirTire.Shop.Presentation.Components;

public class Cart : ViewComponent
{
    private ISender? _mediator;

    public Cart(ISender? mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var orderInfo = await _mediator.Send(new GetCartInfoQuery());
        Guard.Against.Null(orderInfo, nameof(orderInfo));

        return View("/Views/Home/Components/Cart.cshtml", orderInfo);
    }
}