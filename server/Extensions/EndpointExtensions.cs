using Microsoft.AspNetCore.Authentication;

namespace server.cabinet.orleu.kz.Extensions
{
    public static class EndpointExtensions
    {
        /// <summary>
        /// Кастомный редирект на страницу Login.
        /// Так как стандартный Account/Login не переопределяется настройками.
        /// Было принято сделать редирект руками
        /// </summary>
        /// <param name="endpoints"></param>
        public static void MapCabinetRoutes(this IEndpointRouteBuilder endpoints, IConfiguration configuration)
        {
            var authMode = configuration["authentication:mode"] ?? "Internal";
            if (authMode.Equals("External", StringComparison.OrdinalIgnoreCase))
            {
                //endpoints.MapGet("/Account/Login", async context =>
                //{
                //    context.Response.Redirect("/Login");
                //});
            }
            else
            {
                endpoints.MapGet("/Account/Login", context =>
            {
                context.Response.Redirect("/Login");
                return Task.CompletedTask;
            });
            }
        }
    }
}
