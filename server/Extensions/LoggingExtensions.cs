using Serilog.Events;
using Serilog;

namespace server.cabinet.orleu.kz.Extensions
{
    public static class LoggingExtensions
    {
        public static void ConfigureSerilog(HostBuilderContext ctx, LoggerConfiguration lc)
        {
            var logOutputPath = ctx.Configuration["serilog:path"] ?? @"C:\temp\orleu\cabinet\log-.txt";

                lc.MinimumLevel.Information()
                // Подавляем все логи
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Fatal)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Hangfire", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Extensions.Http", LogEventLevel.Fatal)
                // Параллельно вывод в консоль
                //.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}")
                // Пишем в файл с ежедневным роллингом
                // Серия файлов: C:\temp\orleu\cabinet\log-2025-06-12.txt, log-2025-06-13.txt и т.д.
                .WriteTo.File(
                  path: logOutputPath,
                  rollingInterval: RollingInterval.Day,
                  retainedFileCountLimit: 7,      // хранить логи за последнюю неделю
                  shared: true,
                  outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                );
        }
    }
}
