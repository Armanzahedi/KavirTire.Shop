using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Bmsd.KavirTire.IdentityServer.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "وارد کردن نام کاربری الزامیست")]
        public string Username { get; set; }
        [Required(ErrorMessage = "وارد کردن رمز عبور الزامیست")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
