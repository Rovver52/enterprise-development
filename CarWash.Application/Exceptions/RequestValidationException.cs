namespace CarWash.Application.Exceptions;

/// <summary>
/// Значение запроса не позволяет выполнить операцию
/// </summary>
public sealed class RequestValidationException(string field, string message) : Exception(message)
{
    /// <summary>
    /// Поле запроса с ошибкой
    /// </summary>
    public string Field { get; } = field;
}
