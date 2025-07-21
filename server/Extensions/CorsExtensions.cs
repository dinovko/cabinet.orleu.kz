namespace server.cabinet.orleu.kz.Extensions
{
    public static class CorsExtensions
    {
        /// <summary>
        /// Настройка CORS для внешнего Front и для внутреннего
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddConfigurationCors(this IServiceCollection services, IConfiguration configuration)
        {
            var mode = configuration["FRONTEND_MODE"] ?? "Embeded";
            var frontUrl = configuration["FRONTEND_URL"];   // null, если не указан

            services.AddCors(options =>
            {
                options.AddPolicy("DynamicCors", builder =>
                {
                    if (mode.Equals("External", StringComparison.OrdinalIgnoreCase) &&
                        !string.IsNullOrWhiteSpace(frontUrl))
                    {
                        // Разрешаем ровно один внешний источник
                        builder.WithOrigins(frontUrl)
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();       // если нужны куки / авторизация
                    }
                    else
                    {
                        // Встроенный фронт: CORS не нужен, но пусть API
                        // останется «открытым» внутри того же origin
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    }
                });
            });

            return services;
        }
    }
}
