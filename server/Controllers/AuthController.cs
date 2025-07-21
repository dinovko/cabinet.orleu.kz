using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace server.cabinet.orleu.kz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //[HttpGet]
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("~/signin-oidc")]
        //public async Task<IActionResult> ExternalAuthCallback()
        //{
        //    var i = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
        //    return Ok();
        //}
    }
}
