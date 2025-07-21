using server.cabinet.orleu.kz.Interfaces;
using server.cabinet.orleu.kz.Repositories;
using server.cabinet.orleu.kz.Services;
using System.Net.Http.Headers;

namespace server.cabinet.orleu.kz.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient<IHttpService, HttpService>();
            services.AddScoped<IEbds, IEbdsRepository>();
            services.AddScoped<IQR, IQRRepository>();
            //                client =>
            //            {
            //                client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
            //                client.DefaultRequestHeaders.Accept.Clear();
            //                client.DefaultRequestHeaders.Accept.Add(
            //                    new MediaTypeWithQualityHeaderValue("application/json"));
            //            })
            //.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            //{
            //    ClientCertificateOptions = ClientCertificateOption.Manual,
            //    ServerCertificateCustomValidationCallback =
            //        (httpRequestMessage, cert, cetChain, policyErrors) => true // Для разработки
            //});
            //services.AddScoped<IAccount, AccountRepository>();
            //services.AddScoped<IAuth, AuthRepository>();
            //services.AddHttpClient<QrApiService>();

            return services;
        }
    }
}
