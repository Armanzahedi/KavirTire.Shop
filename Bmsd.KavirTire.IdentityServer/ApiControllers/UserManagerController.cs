using Bmsd.KavirTire.IdentityServer.Data;
using Bmsd.KavirTire.IdentityServer.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bmsd.KavirTire.IdentityServer.ApiControllers
{
    [Route("api/[action]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public void InternalAddNewUser(UserRequest user)
        {
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = user.UserName,
            } , user.Password);
        }
    }
}
