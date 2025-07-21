using Hangfire;
using server.cabinet.orleu.kz.Services;

namespace server.cabinet.orleu.kz.Extensions
{
    public static class RecurringJobExtensions
    {
        /// <summary>
        /// Настройка расписания Hangfire
        /// </summary>
        /// <param name="jobManager"></param>
        /// <param name="configuration"></param>
        public static void ConfigureRecurringJobs(this IRecurringJobManager jobManager, IConfiguration configuration)
        {
#if DEBUG
            var ebdsCron = Cron.Daily(11, 31);
#else
            var ebdsCron = configuration["ebds:cron"] ?? Cron.Weekly(DayOfWeek.Sunday,0);
#endif

            jobManager.AddOrUpdate<EBDSRefSyncService>(
                "sync-ebds-refs",
                job => job.SyncData(),
                ebdsCron,
                new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Local,
                    MisfireHandling = MisfireHandlingMode.Strict,
                });
        }
    }
}
