namespace Bmsd.KavirTire.IdentityServer;

public class OpenIddictApplicationConfiguration
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string DisplayName { get; set; }
    public string[] PostLogoutRedirectUris { get; set; }
    public string[] RedirectUris { get; set; }
}