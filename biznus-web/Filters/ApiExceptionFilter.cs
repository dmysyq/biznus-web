using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace biznus_web.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Обрабатываем только API запросы
            if (!context.HttpContext.Request.Path.StartsWithSegments("/api"))
            {
                return;
            }

            _logger.LogError(context.Exception, 
                "API Exception: {Method} {Path}", 
                context.HttpContext.Request.Method,
                context.HttpContext.Request.Path);

            var response = new
            {
                error = new
                {
                    message = context.Exception.Message,
                    details = context.Exception.InnerException?.Message,
                    path = context.HttpContext.Request.Path,
                    timestamp = DateTime.UtcNow
                }
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}

