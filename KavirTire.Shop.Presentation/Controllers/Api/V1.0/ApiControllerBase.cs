using KavirTire.Shop.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KavirTire.Shop.Presentation.Controllers.Api.V1._0;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    
}