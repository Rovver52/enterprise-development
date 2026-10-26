namespace CarWash.Application.Exceptions;

/// <summary>
/// Запрошенная сущность не найдена
/// </summary>
public sealed class EntityNotFoundException(string message) : Exception(message);
