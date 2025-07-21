using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.cabinet.orleu.kz.Interfaces;

namespace server.cabinet.orleu.kz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EbdsController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEbds _repo;
        public EbdsController(
            IHttpContextAccessor contextAccessor,
            IEbds repo
            )
        {
            _contextAccessor = contextAccessor;
            _repo = repo;
        }

        [HttpGet("userprofile")]
        [ResponseCache(Duration = 3600)]
        public async Task<IActionResult> Get()
        {
            var currentUser = _contextAccessor?.HttpContext?.User?.Identity?.Name;
            if (String.IsNullOrEmpty(currentUser))
            {
                return Unauthorized();
            }

            return Ok(await _repo.GetResponseUserprofile(currentUser));
        }
    }
}
