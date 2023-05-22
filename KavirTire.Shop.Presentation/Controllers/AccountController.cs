using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KavirTire.Shop.Presentation.Controllers;

[AllowAnonymous]
public class AccountController : BaseController
{
    public IActionResult Login()
    {
        var returnUrl = Url.Action("Index", "Home");
        return Challenge(new AuthenticationProperties { RedirectUri = returnUrl });
    }
    
    [Route("signin-oidc")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Callback()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (result?.Succeeded == true)
        {
            return RedirectToAction("Index", "Home");
        }
        return RedirectToAction("Login");
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}