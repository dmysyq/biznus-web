using biznus_web.Models;
using biznus_web.Middleware;
using biznus_web.Filters;

using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Настройка Serilog для Seq (используем конфигурацию из appsettings.json)
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq(
        serverUrl: context.Configuration["Serilog:WriteTo:1:Args:serverUrl"] ?? 
                   Environment.GetEnvironmentVariable("SEQ_URL") ?? 
                   "http://localhost:5341")
);


builder.Services.AddControllersWithViews(options =>
    {
        options.Filters.Add<ApiExceptionFilter>();
        options.Filters.Add<ApiLoggingFilter>();
    })
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<biznus_web.Services.TranslationService>();
builder.Services.AddScoped<biznus_web.Services.JwtService>();
builder.Services.AddScoped<biznus_web.Services.CartService>();
builder.Services.AddHttpClient<biznus_web.Services.ApiClientService>();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCulture = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ru-RU"),
        new CultureInfo("kk-KZ"),
        new CultureInfo("fr-FR")
    };

    options.DefaultRequestCulture = new RequestCulture(culture: "ru-RU", uiCulture: "ru-RU");
    options.SupportedCultures = supportedCulture;
    options.SupportedUICultures = supportedCulture;
   
});

// Настройка JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "BiznusWeb";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "BiznusWebUsers";

builder.Services.AddAuthentication(options =>
    {
        // Cookie по умолчанию для MVC
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.SlidingExpiration = true;
        options.Cookie.Name = "BiznusAuth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Lax;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



var app = builder.Build();

// Middleware для логирования запросов
app.UseMiddleware<RequestLoggingMiddleware>();

// Глобальный обработчик исключений
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

var supportedCulture = new[] { "en-US", "kk-KZ", "ru-RU", "fr-FR" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("ru-RU")
    .AddSupportedCultures(supportedCulture)
    .AddSupportedUICultures(supportedCulture);

app.UseRequestLocalization(localizationOptions);


app.UseRouting();

// Добавляем аутентификацию и авторизацию
app.UseAuthentication();
app.UseAuthorization();

// Добавляем поддержку сессий
app.UseSession();

// Map API routes
app.MapControllers();

// Map MVC routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

try
{
    Log.Information("Starting BiznusWeb application");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

