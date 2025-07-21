using Hangfire;
using Serilog;
using server.cabinet.orleu.kz.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddRazorPages();

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
//builder.WebHost.UseUrls("http://0.0.0.0:80");
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
