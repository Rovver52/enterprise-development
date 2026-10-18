namespace CarWash.Domain.Models;

/// <summary>
/// Сущность с целочисленным идентификатором
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    int Id { get; set; }
}
