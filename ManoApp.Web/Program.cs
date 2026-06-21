using ManoApp.Application.Services;
using ManoApp.Domain.Interfaces;
using ManoApp.Domain.Services;
using ManoApp.Infrastructure.Persistence;
using ManoApp.Web.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGestureClassifier, GestureClassifier>();

var logsFolder = Path.Combine(builder.Environment.ContentRootPath, "data", "logs");
builder.Services.AddSingleton<IGestureLogRepository>(new CsvGestureLogRepository(logsFolder));

builder.Services.AddScoped<GestureLoggingService>();

// Authentication
builder.Services.Configure<List<AppUser>>(builder.Configuration.GetSection("Users"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthentication(); // New line

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();