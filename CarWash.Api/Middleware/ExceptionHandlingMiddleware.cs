using CarWash.Api.Errors;
using CarWash.Application.Exceptions;

namespace CarWash.Api.Middleware;

/// <summary>
/// Преобразование ошибок приложения в HTTP-ответы и запись исключений в журнал
/// </summary>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    /// <summary>
    /// Обработать запрос и перехватить исключения следующих компонентов
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception) when (!context.Response.HasStarted)
        {
            var status = exception switch
            {
                EntityNotFoundException => StatusCodes.Status404NotFound,
                EntityConflictException => StatusCodes.Status409Conflict,
                RequestValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            if (status == StatusCodes.Status500InternalServerError)
                logger.LogError(exception, "Ошибка при обработке {Method} {Path}; TraceId: {TraceId}",
                    context.Request.Method, context.Request.Path, context.TraceIdentifier);
            else
                logger.LogWarning("HTTP {Status}: {Message}; {Method} {Path}; TraceId: {TraceId}",
                    status, exception.Message, context.Request.Method, context.Request.Path, context.TraceIdentifier);

            var message = status == StatusCodes.Status500InternalServerError
                ? "Внутренняя ошибка сервера."
                : exception.Message;
            var errors = exception is RequestValidationException validation
                ? new Dictionary<string, string[]> { [validation.Field] = [validation.Message] }
                : null;
            context.Response.Clear();
            context.Response.StatusCode = status;
            await context.Response.WriteAsJsonAsync(ErrorResponseFactory.Create(context, status, message, errors));
        }
    }
}
