using Microsoft.AspNetCore.Mvc.Filters;

namespace biznus_web.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Логируем только API запросы
            if (!context.HttpContext.Request.Path.StartsWithSegments("/api"))
            {
                return;
            }

            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            _logger.LogInformation(
                "API Action Executing: {Controller}.{Action} - Method: {Method}",
                controllerName,
                actionName,
                context.HttpContext.Request.Method);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Логируем только API запросы
            if (!context.HttpContext.Request.Path.StartsWithSegments("/api"))
            {
                return;
            }

            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            if (context.Exception == null)
            {
                _logger.LogInformation(
                    "API Action Executed: {Controller}.{Action} - Status: {StatusCode}",
                    controllerName,
                    actionName,
                    context.HttpContext.Response.StatusCode);
            }
        }
    }
}

