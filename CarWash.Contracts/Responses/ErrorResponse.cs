namespace CarWash.Contracts.Responses;

/// <summary>
/// Единый формат ошибки API
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// HTTP-код ошибки
    /// </summary>
    public required int Status { get; set; }

    /// <summary>
    /// Сообщение для пользователя
    /// </summary>
    public required string Message { get; set; }

    /// <summary>
    /// Идентификатор запроса для поиска в журнале
    /// </summary>
    public required string TraceId { get; set; }

    /// <summary>
    /// Ошибки отдельных полей запроса
    /// </summary>
    public required Dictionary<string, string[]> Errors { get; set; }
}
