using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using server.cabinet.orleu.kz.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace server.cabinet.orleu.kz.Pages
{
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [BindProperty]
        public string? DigSign { get; set; }
        public IActionResult OnGet()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/Profile" }, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
