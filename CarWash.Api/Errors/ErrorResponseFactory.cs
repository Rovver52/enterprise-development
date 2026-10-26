using CarWash.Contracts.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Errors;

internal static class ErrorResponseFactory
{
    public static ErrorResponse Create(HttpContext context, int status, string message,
        Dictionary<string, string[]>? errors = null) => new()
    {
        Status = status,
        Message = message,
        TraceId = context.TraceIdentifier,
        Errors = errors ?? []
    };

    public static IActionResult InvalidModelState(ActionContext context)
    {
        var errors = context.ModelState
            .Where(item => item.Value?.Errors.Count > 0)
            .ToDictionary(item => item.Key, item => item.Value!.Errors
                .Select(error => error.Exception is null && !string.IsNullOrEmpty(error.ErrorMessage)
                    && !string.IsNullOrEmpty(item.Key) && !item.Key.StartsWith('$')
                        ? error.ErrorMessage
                        : "Некорректное или отсутствующее значение.")
                .ToArray());
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>()
            .CreateLogger("CarWash.Api.Validation");
        logger.LogWarning("Ошибка проверки запроса {Method} {Path}; TraceId: {TraceId}",
            context.HttpContext.Request.Method, context.HttpContext.Request.Path,
            context.HttpContext.TraceIdentifier);
        return new BadRequestObjectResult(Create(context.HttpContext, StatusCodes.Status400BadRequest,
            "Проверьте данные запроса.", errors));
    }

    public static async Task WriteStatusCodeAsync(StatusCodeContext context)
    {
        var http = context.HttpContext;
        var status = http.Response.StatusCode;
        var message = status switch
        {
            StatusCodes.Status400BadRequest => "Некорректный запрос.",
            StatusCodes.Status404NotFound => "Ресурс не найден.",
            StatusCodes.Status405MethodNotAllowed => "Метод запроса не поддерживается.",
            StatusCodes.Status415UnsupportedMediaType => "Используйте формат application/json.",
            _ => "Не удалось обработать запрос."
        };
        var logger = http.RequestServices.GetRequiredService<ILoggerFactory>()
            .CreateLogger("CarWash.Api.HttpErrors");
        logger.LogWarning("HTTP {Status} для {Method} {Path}; TraceId: {TraceId}",
            status, http.Request.Method, http.Request.Path, http.TraceIdentifier);
        await http.Response.WriteAsJsonAsync(Create(http, status, message));
    }
}
