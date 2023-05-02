namespace KavirTire.Shop.Application.Common.Services;

public interface ICurrentUser
{
    Guid? UserId { get; }
}