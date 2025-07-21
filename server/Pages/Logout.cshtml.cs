using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace server.cabinet.orleu.kz.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            // 1. Выход из приложения (удаление куки)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 2. Перенаправление на Keycloak для глобального выхода (если нужно)
            return SignOut(
                new AuthenticationProperties { }, // Куда вернуться после выхода
                OpenIdConnectDefaults.AuthenticationScheme // Выход из Keycloak
            );
        }
    }
}
