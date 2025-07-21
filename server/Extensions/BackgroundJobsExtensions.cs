using Hangfire;
using Hangfire.PostgreSql;

namespace server.cabinet.orleu.kz.Extensions
{
    public static class BackgroundJobsExtensions
    {
        public static IServiceCollection AddConfiguredHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config =>
            {
                var constring = configuration.GetConnectionString("DefaultConnection");
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UsePostgreSqlStorage(options =>
                {
                    options.UseNpgsqlConnection(constring);
                }
                , new PostgreSqlStorageOptions
                {
                    SchemaName = "hangfire",
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromSeconds(15),
                });
            });

            services.AddHangfireServer();

            return services;
        }
    }
}
