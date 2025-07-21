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
            // 1. ����� �� ���������� (�������� ����)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 2. ��������������� �� Keycloak ��� ����������� ������ (���� �����)
            return SignOut(
                new AuthenticationProperties { }, // ���� ��������� ����� ������
                OpenIdConnectDefaults.AuthenticationScheme // ����� �� Keycloak
            );
        }
    }
}
