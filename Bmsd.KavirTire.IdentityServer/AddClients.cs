using Bmsd.KavirTire.IdentityServer.Data;
using Bmsd.KavirTire.IdentityServer.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;


namespace Bmsd.KavirTire.IdentityServer
{
    public class AddClients : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public AddClients(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            var configuration = _serviceProvider.GetRequiredService<IConfiguration>(); 
            var openIddictApplications = configuration.GetSection("OpenIddict:Applications")
                .Get<List<OpenIddictApplicationConfiguration>>();
            
            foreach (var applicationConfig in openIddictApplications)
            {
                if (await manager.FindByClientIdAsync(applicationConfig.ClientId) == null)
                {
                    var application = new OpenIddictApplicationDescriptor
                    {
                        ClientId = applicationConfig.ClientId,
                        ClientSecret = applicationConfig.ClientSecret,
                        DisplayName = applicationConfig.DisplayName,
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Authorization,
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.Endpoints.Logout,
                            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                            OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                            OpenIddictConstants.Permissions.GrantTypes.Implicit,
                            OpenIddictConstants.Permissions.Prefixes.Scope + "openid",
                            OpenIddictConstants.Permissions.Prefixes.Scope + "profile",
                            OpenIddictConstants.Permissions.ResponseTypes.IdToken,
                            OpenIddictConstants.Permissions.ResponseTypes.Token,
                            OpenIddictConstants.Permissions.ResponseTypes.Code,
                        },
                    };
                    
                    foreach (var applicationConfigPostLogoutRedirectUri in applicationConfig.PostLogoutRedirectUris)
                        application.PostLogoutRedirectUris.Add(new Uri(applicationConfigPostLogoutRedirectUri));
                    
                    foreach (var redirectUri in applicationConfig.RedirectUris)
                        application.RedirectUris.Add(new Uri(redirectUri));
                    
                    await manager.CreateAsync(application);
                }
            }

            var userMan = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await userMan.CreateAsync(new ApplicationUser
            {
                UserName = "3240384205",

            }, "Aa123456");

            var res = await userMan.CreateAsync(new ApplicationUser
            {
                UserName = "0089844718",

            }, "Aa123456");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
