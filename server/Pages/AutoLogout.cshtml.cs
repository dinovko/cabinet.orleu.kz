using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace server.cabinet.orleu.kz.Pages
{
    public class AutoLogoutModel : PageModel
    {
        private readonly IAntiforgery _antiforgery;

        public string AntiForgeryToken { get; private set; }

        public AutoLogoutModel(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void OnGet()
        {
            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
            AntiForgeryToken = tokens.RequestToken;
        }
    }
}
