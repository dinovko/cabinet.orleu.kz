using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.cabinet.orleu.kz.Interfaces;

namespace server.cabinet.orleu.kz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QRController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IQR _repo;
        public QRController(IHttpContextAccessor contextAccessor, IQR repo)
        {
            _contextAccessor = contextAccessor;
            _repo = repo;
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetGroups(int? page =1, int? size = 20)
        {
            var currentUser = _contextAccessor?.HttpContext?.User?.Identity?.Name ?? null;
            if (currentUser == null) { return Unauthorized("Пользователь не найден"); }

            return Ok(await _repo.GetGroups(currentUser, page, size));
        }
    }
}
