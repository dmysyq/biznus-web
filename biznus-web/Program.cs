using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

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
    
    options.RequestCultureProviders.Clear();
    options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
    options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());
    options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
});

// Настройка аутентификации через cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.SlidingExpiration = true;
        options.Cookie.Name = "BiznusAuth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

// Добавляем сервисы для работы с сессиями
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

var supportedCulture = new[]
{
    "en-US", "ru-RU", "kk-KZ", "fr-FR"
};
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
