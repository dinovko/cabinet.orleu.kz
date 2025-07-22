using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace server.cabinet.orleu.kz.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;

        public bool IsExternalFront { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public void OnGet() 
        {
            bool IsAuth = User != null && User.Identity != null && User.Identity.IsAuthenticated;
            IsExternalFront = !String.IsNullOrEmpty(_config["FRONTEND_MODE"]) ? _config["FRONTEND_MODE"].Equals("External",StringComparison.OrdinalIgnoreCase) : false;

            Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = TimeSpan.FromHours(1) // Кэшировать 1 час
            };
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
