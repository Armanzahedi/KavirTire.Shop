using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KavirTire.Shop.Infrastructure.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddKavirAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = "KavirTireShop.AuthCookie";
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
            })
            .AddOpenIdConnect(options =>
            {
                options.Authority = configuration["Authentication:Authority"];
                options.ClientId = configuration["Authentication:ClientId"];
                options.ClientSecret = configuration["Authentication:ClientSecret"];
                options.ResponseType = "code";
                options.CallbackPath = "/signin-oidc";
                options.SaveTokens = true;
                options.RequireHttpsMetadata = false;
                options.Scope.Add("openid");
            });

        return services;
    }

}