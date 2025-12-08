using System.Net;
using System.Text.Json;

namespace biznus_web.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred. Request Path: {Path}", 
                    context.Request.Path);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                error = new
                {
                    message = "An error occurred while processing your request.",
                    details = exception.Message,
                    path = context.Request.Path,
                    timestamp = DateTime.UtcNow
                }
            };

            // Для API запросов возвращаем JSON
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                return context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }

            // Для обычных запросов перенаправляем на страницу ошибки
            context.Response.Redirect("/Home/Error");
            return Task.CompletedTask;
        }
    }
}

