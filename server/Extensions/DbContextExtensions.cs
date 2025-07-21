using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using server.cabinet.orleu.kz.Data;
using server.cabinet.orleu.kz.Models;
using Serilog;

namespace server.cabinet.orleu.kz.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Подключение к базе данных с 5 попытками переподключения
        /// на случай если база на момент запуска сервера еще не готова
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CabinetDbContext>(o =>
                o.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorCodesToAdd: null);
                }
            ));
        }

        /// <summary>
        /// Применение миграций
        /// </summary>
        /// <param name="app"></param>
        /// <param name="maxRetries"></param>
        /// <param name="delayMilliseconds"></param>
        public static void ApplyMigrationsWithRetry(this IApplicationBuilder app, int maxRetries = 10, int delayMilliseconds = 2000)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<CabinetDbContext>();

            while (maxRetries > 0)
            {
                try
                {
                    db.Database.Migrate();
                    Log.Information("✅ Миграции успешно применены.");
                    break;
                }
                catch (PostgresException ex) when (ex.SqlState == "57P03") // "database is starting up"
                {
                    maxRetries--;
                    Log.Warning("⏳ Postgres ещё не готов. Повторная попытка через {Delay} мс... Осталось попыток: {Retries}", delayMilliseconds, maxRetries);
                    Thread.Sleep(delayMilliseconds);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "❌ Ошибка при применении миграций. Попытка №{Attempt}", 10 - maxRetries + 1);
                    throw;
                }
            }

            db.Database.Migrate(); // последняя попытка
        }
    }
}
