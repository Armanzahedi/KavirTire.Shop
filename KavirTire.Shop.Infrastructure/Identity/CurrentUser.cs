using EasyCaching.Core;
using KavirTire.Shop.Application.Common.Services;
using KavirTire.Shop.Infrastructure.Cache;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace KavirTire.Shop.Infrastructure.Identity;

public class CurrentUser : ICurrentUser
{
    private readonly AppDbContext _dbContext;
    private readonly IEasyCachingProvider _cachingProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUser(AppDbContext dbContext, IEasyCachingProvider cachingProvider, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _cachingProvider = cachingProvider;
        _httpContextAccessor = httpContextAccessor;
    }
    public Guid? UserId => RetrieveUserId();
    
    private Guid? RetrieveUserId()
    {
        ClaimsIdentity? identity = (ClaimsIdentity)_httpContextAccessor?.HttpContext?.User?.Identity;

        // Retrieve the user's unique identifier
        string? userName = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userName))
            throw new Exception("Current User was no found");
        return _cachingProvider.GetOrCreate<Guid?>($"u-{userName}",() =>
        {
            return _dbContext.Customers.Select(x=>new {x.Id,x.Username}).FirstOrDefault(x => x.Username == userName)?.Id;
        });
    }
}