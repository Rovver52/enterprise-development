using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarWash.Api.Filters;

/// <summary>
/// Преобразование ошибок операций приложения в HTTP-ответы
/// </summary>
public class ApplicationExceptionFilter : IExceptionFilter
{
    /// <inheritdoc />
    public void OnException(ExceptionContext context)
    {
        var status = context.Exception switch
        {
            KeyNotFoundException => StatusCodes.Status404NotFound,
            ArgumentException => StatusCodes.Status400BadRequest,
            InvalidOperationException => StatusCodes.Status409Conflict,
            _ => 0
        };
        if (status == 0)
            return;

        context.Result = new ObjectResult(new ProblemDetails
        {
            Status = status,
            Title = context.Exception.Message
        })
        {
            StatusCode = status,
            ContentTypes = { "application/problem+json" }
        };
        context.ExceptionHandled = true;
    }
}
