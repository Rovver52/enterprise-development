namespace CarWash.Application.Exceptions;

/// <summary>
/// Операция нарушает существующие связи сущностей
/// </summary>
public sealed class EntityConflictException(string message) : Exception(message);
