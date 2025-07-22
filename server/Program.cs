using Hangfire;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Serilog;
using server.cabinet.orleu.kz.Extensions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

#region �����������
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllers();

builder.Services.AddRazorPages().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
   {
        new CultureInfo("ru"),
        new CultureInfo("kk"),
        new CultureInfo("en")
    };
    options.DefaultRequestCulture = new RequestCulture("kk");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
#endregion

//��������� ����������� ����� KeyCloak
builder.Services.AddKeycloakAuthentication(builder.Configuration);

#if !DEBUG
//������������ ����� ����� Seilog
builder.Host.UseSerilog(server.cabinet.orleu.kz.Extensions.LoggingExtensions.ConfigureSerilog);
#endif

//��� ������ ������� ������, ���� React ������� ��� ����������, �� �������� CORS
builder.Services.AddConfigurationCors(builder.Configuration);

//��������� ������������� � ���������� ��������� �������
builder.Services.AddConfiguredHangfire(builder.Configuration);

//��� ��� �������
builder.Services.AddConfigureDbContext(builder.Configuration);

//��� ������� � ����, ����� Interfaces ������ �����
builder.Services.AddRepositories();

#if DEBUG
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
#endif

//������ ������ �� ����������� ���� �� ��������� ����
#if !DEBUG
builder.WebHost.UseUrls("http://0.0.0.0:80");
#endif
var app = builder.Build();
//����������� ��� �������
app.ApplyMigrationsWithRetry();

//������� �� Hangfire,  ������ �� �������
app.UseHangfireDashboard();

//������ ������� � Extensions
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
        // ��� ����������� ������
        ctx.Context.Response.Headers.Append(
            "Cache-Control", "public,max-age=2592000");
    }
});

var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app.UseRouting();


app.UseCors();
//����������� ����� ��� ����������� ���� ����� KeyCloak, ���� Identity
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapDefaultControllerRoute();
app.MapRazorPages();

//���� ��������, ���� ��� ���������� ����������� ����� ���
//app.MapCabinetRoutes(app.Configuration);
//����������� ������ �� ������ ������� � wwwRoot
app.MapFallbackToFile("index.html").RequireAuthorization();

app.Run();
