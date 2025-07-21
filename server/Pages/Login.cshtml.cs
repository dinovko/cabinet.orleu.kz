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
        //private readonly IAuth _repo;
        //private readonly IConfiguration _config;
        //public LoginModel(IAuth repo, IConfiguration config)
        //{
        //    _repo = repo;
        //    _config = config;
        //}

        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [BindProperty]
        public string? DigSign { get; set; }
        public async Task<IActionResult> OnGet()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, OpenIdConnectDefaults.AuthenticationScheme);
            //var authMode = _config["authentication:mode"] ?? "Internal";
            //if (authMode.Equals("External", StringComparison.OrdinalIgnoreCase))
            //{
            //    return Challenge(new AuthenticationProperties { RedirectUri = "/" }, OpenIdConnectDefaults.AuthenticationScheme);
            //}

            //if (User?.Identity?.IsAuthenticated == true)
            //{
            //    return Redirect("/");
            //}

            //Username = null;
            //Password = null;
            //DigSign = null;

            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //HttpContext.Session.Clear();
            //foreach (var cookie in Request.Cookies.Keys)
            //{
            //    Response.Cookies.Delete(cookie);
            //}

            //return Page();
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if ((String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password)) && String.IsNullOrEmpty(DigSign))
        //    {
        //        ModelState.AddModelError("Password", "¬ведите логин и пароль или войдите с помощью Ё÷ѕ");

        //        return Page();
        //    }

        //    var signResult = new DTOs.AuthDto();

        //    if (!String.IsNullOrEmpty(DigSign))
        //    {
        //        signResult = await _repo.SignIn(DigSign);
        //    }

        //    if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password))
        //    {
        //        signResult = await _repo.SignIn(Username, Password);
        //    }

        //    if (!signResult.IsSuccess)
        //    {
        //        ModelState.AddModelError("Password", signResult.ErrorMessage ?? "ќшибка при авторизации");
        //    }
        //    else
        //    {
        //        var frontMode = _config["FRONTEND_MODE"] ?? "Embeded";
        //        var frontURL = _config["FRONTEND_URL"] ?? "http://localhost:5003";

        //        if (frontMode.Equals("Embeded", StringComparison.OrdinalIgnoreCase))
        //        {
        //            return Redirect("/");
        //        }
        //        else
        //        {
        //            return Redirect(frontURL);
        //        }

        //    }



        //    return Page();
        //}
    }
}
