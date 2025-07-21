using System.Security.Claims;

namespace server.cabinet.orleu.kz.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Возвращает userId (ClaimTypes.NameIdentifier) либо null,
        /// не привязываясь к UserManager.
        /// </summary>
        public static Guid? GetUserId(this ClaimsPrincipal? user)
        {
            if (user != null)
            {
                return Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            return null;
        }
    }
}
