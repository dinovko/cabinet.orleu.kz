using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Serilog;
using System.Text.RegularExpressions;

namespace server.cabinet.orleu.kz.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IStringLocalizer<IndexModel> _localizer;

        public bool IsExternalFront { get; private set; }

        public IndexModel(IConfiguration config
            , IStringLocalizer<IndexModel> localizer
            )
        {
            _config = config;
            _localizer = localizer;
        }

        public string About { get; set; }
        public string AboutText { get; set; }
        public string AccessPoint { get; set; }
        public string Contacts { get; set; }
        public string Dashboard { get; set; }
        public string EBDP { get; set; }
        public string EBDPDescription { get; set; }
        public string Links { get; set; }
        public string LMS { get; set; }
        public string LMSDescription { get; set; }
        public string Login { get; set; }
        public string Socials { get; set; }


        public async Task<IActionResult> OnGetAsync(string culture)
        {
            #region Локализированные сообщения
            Login = _localizer["Login"];
            About = _localizer["About"];
            AboutText = _localizer["AboutText"];
            AccessPoint = _localizer["AccessPoint"];
            Contacts = _localizer["Contacts"];
            Dashboard = _localizer["Dashboard"];
            EBDP = _localizer["EBDP"];
            EBDPDescription = _localizer["EBDPDescription"];
            Links = _localizer["Links"];
            LMS = _localizer["LMS"];
            LMSDescription = _localizer["LMSDescription"];
            Login = _localizer["Login"];
            Socials = _localizer["Socials"];
            #endregion

            bool IsAuth = User != null && User.Identity != null && User.Identity.IsAuthenticated;
            IsExternalFront = !String.IsNullOrEmpty(_config["FRONTEND_MODE"]) ? _config["FRONTEND_MODE"].Equals("External", StringComparison.OrdinalIgnoreCase) : false;

            if (!string.IsNullOrWhiteSpace(culture))
            {
                Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

                // Обновляем текущий URL без culture-параметра
                var currentUrl = Url.Page("./Index");
                return LocalRedirect(currentUrl);
            }

            return Page();

            //эта хуйта которую сказал ChatGPT валит нахуй весь проект через Docker
            //Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
            //{
            //    Public = true,
            //    MaxAge = TimeSpan.FromHours(1) // Кэшировать 1 час
            //};
        }

        //public ActionResult OnGet()
        //{
        //    var authMode = _config["authentication:mode"] ?? "External";
        //    var frontMode = _config["FRONTEND_MODE"] ?? "Embeded";
        //    var frontURL = _config["FRONTEND_URL"] ?? "http://localhost:5003";

        //    bool IsExternalAuth = authMode.Equals("External", StringComparison.OrdinalIgnoreCase);
        //    bool IsEmbeded = frontMode.Equals("Embeded", StringComparison.OrdinalIgnoreCase);
        //    bool IsAuth = User != null && User.Identity != null && User.Identity.IsAuthenticated;

        //    if (IsAuth == true)
        //    {
        //        return IsEmbeded ? Redirect("/") : Redirect(frontURL);
        //    }

        //    return Page();

        //    //return IsExternalAuth ? Redirect("/Account/Login") : Redirect("Login");
        //}
    }
}
