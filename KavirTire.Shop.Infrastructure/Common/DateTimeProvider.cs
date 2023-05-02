using KavirTire.Shop.Application.Common.Services;

namespace KavirTire.Shop.Infrastructure.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow  => DateTime.Now.ToUniversalTime();
}