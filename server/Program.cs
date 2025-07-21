using Hangfire;
using Serilog;
using server.cabinet.orleu.kz.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddRazorPages();

//настройка авторизации через KeyCloak
builder.Services.AddKeycloakAuthentication(builder.Configuration);

#if !DEBUG
//логгирование всего через Seilog
builder.Host.UseSerilog(server.cabinet.orleu.kz.Extensions.LoggingExtensions.ConfigureSerilog);
#endif

//Для разных режимов работы, если React запущен для разработки, то добавлен CORS
builder.Services.AddConfigurationCors(builder.Configuration);

//Настройка синхронизации с повторными попытками запуска
builder.Services.AddConfiguredHangfire(builder.Configuration);

//тут все понятно
builder.Services.AddConfigureDbContext(builder.Configuration);

//все вытащил в репы, папка Interfaces хранит связь
builder.Services.AddRepositories();

#if DEBUG
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
#endif

//ибаная залупа не запускается если не указывать явно
//builder.WebHost.UseUrls("http://0.0.0.0:80");
var app = builder.Build();
//мигрируемся при запуске
app.ApplyMigrationsWithRetry();

//Дашборд от Hangfire,  открыт не защищен
app.UseHangfireDashboard();

//Сервис вытащен в Extensions
var jobManager = app.Services.GetRequiredService<IRecurringJobManager>();
jobManager.ConfigureRecurringJobs(app.Configuration);

app.UseCors("DynamicCors");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // все закэшировал внахуй
        ctx.Context.Response.Headers.Append(
            "Cache-Control", "public,max-age=2592000");
    }
});

app.UseRouting();

app.UseCors();
//обязательно нужно для авторизации хоть через KeyCloak, хоть Identity
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapDefaultControllerRoute();
app.MapRazorPages();

//пока отключил, юзал при встроенной авторизации через ЭЦП
//app.MapCabinetRoutes(app.Configuration);
//Возвращемся всегда на фронту который в wwwRoot
app.MapFallbackToFile("index.html").RequireAuthorization();

app.Run();
