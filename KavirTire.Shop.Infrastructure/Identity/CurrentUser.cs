using KavirTire.Shop.Application.Common.Services;

namespace KavirTire.Shop.Infrastructure.Identity;

public class CurrentUser : ICurrentUser
{
    public Guid? UserId { get => Guid.Parse("96661064-BB1B-ED11-81DA-0050569ABF13"); }
}