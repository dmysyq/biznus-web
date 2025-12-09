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
var seqUrl = builder.Configuration["Serilog:WriteTo:1:Args:serverUrl"] ?? 
             Environment.GetEnvironmentVariable("SEQ_URL") ?? 
             "http://localhost:5341";

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "BiznusWeb")
    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .WriteTo.Seq(
        serverUrl: seqUrl,
        restrictedToMinimumLevel: LogEventLevel.Information)
);


builder.Services.AddControllersWithViews(options =>
    {
        options.Filters.Add<ApiExceptionFilter>();
        options.Filters.Add<ApiLoggingFilter>();
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<biznus_web.Services.TranslationService>();
builder.Services.AddScoped<biznus_web.Services.JwtService>();
builder.Services.AddScoped<biznus_web.Services.CartService>();
builder.Services.AddHttpClient<biznus_web.Services.ApiClientService>();
builder.Services.AddHttpContextAccessor();


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

// Настройка локализации для БД (только для определения культуры, не для resx)
var supportedCulture = new[] { "en-US", "kk-KZ", "ru-RU", "fr-FR" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("ru-RU")
    .AddSupportedCultures(supportedCulture)
    .AddSupportedUICultures(supportedCulture);
localizationOptions.RequestCultureProviders.Clear();
localizationOptions.RequestCultureProviders.Add(new CookieRequestCultureProvider());
localizationOptions.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());
localizationOptions.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
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
    Log.Information("=== BiznusWeb Application Starting ===");
    Log.Information("Seq URL: {SeqUrl}", seqUrl);
    Log.Information("Environment: {Environment}", app.Environment.EnvironmentName);
    Log.Information("Application started successfully");
    
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    throw;
}
finally
{
    Log.Information("=== BiznusWeb Application Shutting Down ===");
    Log.CloseAndFlush();
}

