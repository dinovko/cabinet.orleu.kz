using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using server.cabinet.orleu.kz.Models;

namespace server.cabinet.orleu.kz.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authMode = configuration["authentication:mode"] ?? "Internal";

            var externalClientUrl = configuration["authentication:external_client_url"] ?? "";

            bool IsExternalFront = authMode.Equals("External", StringComparison.OrdinalIgnoreCase);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, (options) =>
            {
                var config = configuration
                            .GetSection("authentication")
                            .Get<KeyCloakAuthenticationOptions>();

                options.Authority = config.authority;
                options.ClientId = config.client_id;
                options.ClientSecret = config.client_secret;
                options.ResponseType = "code";

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;

                options.TokenValidationParameters.NameClaimType = "preferred_username";
                options.TokenValidationParameters.RoleClaimType = "roles";

                options.RequireHttpsMetadata = false;

                options.Events = new OpenIdConnectEvents
                {
                    OnTicketReceived = context =>
                    {
                        if (context != null && context.Properties != null)
                        {
                            context.Properties.RedirectUri = IsExternalFront == true ? externalClientUrl : "/index.html";
                        }

                        return Task.CompletedTask;
                    },

                    OnAuthorizationCodeReceived = context =>
                    {
                        if (context != null && context.Properties != null)
                        {
                            context.Properties.RedirectUri = IsExternalFront
                            ? externalClientUrl
                            : "/Profile";
                        }
                        return Task.CompletedTask;
                    },
                };

            });

            return services;
        }
        public static void ConfigureAuthentication(Microsoft.AspNetCore.Authentication.AuthenticationOptions o, IConfiguration configuration)
        {
            o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        }

        public static void ConfigureCookieAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationOptions o, IConfiguration configuration)
        {
            var authMode = configuration["authentication:mode"] ?? "Internal";
            if (!authMode.Equals("External", StringComparison.OrdinalIgnoreCase))
            {
                //o.Cookie.Name = ".AspNetCore.Identity.Application";
                o.ExpireTimeSpan = TimeSpan.FromHours(9);
                o.LoginPath = "/Login";
                o.LogoutPath = "/Logout";
                o.SlidingExpiration = true;
#if DEBUG
                o.Cookie.HttpOnly = false;
                o.Cookie.SecurePolicy = CookieSecurePolicy.None;
                o.Cookie.SameSite = SameSiteMode.Lax;
#else
        o.Cookie.HttpOnly = true;
        o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        o.Cookie.SameSite = SameSiteMode.Strict;
#endif

            }


            #region API
            o.Events.OnCheckSlidingExpiration = ctx =>
            {
                var i = ctx;
                return Task.CompletedTask;
            };

            o.Events.OnRedirectToLogin = ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase) ||
                    ctx.Request.Headers.Accept.ToString().Contains("application/json", StringComparison.OrdinalIgnoreCase))
                {
                    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
                else
                {
                    ctx.Response.Redirect(ctx.RedirectUri);
                }
                return Task.CompletedTask;
            };

            o.Events.OnRedirectToAccessDenied = ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase) ||
                    ctx.Request.Headers.Accept.ToString().Contains("application/json", StringComparison.OrdinalIgnoreCase))
                {
                    ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                }
                else
                {
                    ctx.Response.Redirect(ctx.RedirectUri);
                }
                return Task.CompletedTask;
            };
            #endregion
        }

        /// <summary>
        /// Конфигурирование на KeyCloak авторизацию
        /// </summary>
        /// <param name="o"></param>
        public static void ConfigureOpenIdConnectAuthentication(Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions o, IConfiguration configuration)
        {
            o.Authority = "http://localhost:8080/realms/orleu2";
            o.ClientId = "mvc-1";
            o.ClientSecret = "pTGsodXchyncUcEgZxMsVMFPc1Bni6IK"; // из Keycloak
            o.ResponseType = "code";

            o.SaveTokens = true;
            o.GetClaimsFromUserInfoEndpoint = true;

            o.TokenValidationParameters.NameClaimType = "preferred_username";
            o.TokenValidationParameters.RoleClaimType = "roles";

            o.RequireHttpsMetadata = false; 
            // Для разработки

            //            var authMode = configuration["authentication:mode"] ?? "Internal";
            //            if (authMode.Equals("External", StringComparison.OrdinalIgnoreCase))
            //            {
            //                var ClientId = configuration["authentication:client_id"] ?? "";
            //                var ClientSecret = configuration["authentication:client_secret"] ?? "";
            //                var Authority = configuration["authentication:authority"] ?? "";

            //                o.Authority = Authority;
            //                o.ClientId = ClientId;
            //                o.ClientSecret = ClientSecret;

            //                o.ResponseType = "code";
            //#if DEBUG
            //                o.RequireHttpsMetadata = false;
            //#endif
            //                o.SaveTokens = true;
            //                o.GetClaimsFromUserInfoEndpoint = true;

            //                o.TokenValidationParameters.NameClaimType = "preferred_username";
            //                o.TokenValidationParameters.RoleClaimType = "roles";

            //o.Scope.Add("openid");
            //o.Scope.Add("profile");
            //o.Scope.Add("email");

            //o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //{
            //    NameClaimType = "preferred_username",
            //    RoleClaimType = "roles"
            //};
        }
    }
}
