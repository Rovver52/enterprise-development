namespace CarWash.Domain.Models;

/// <summary>
/// Услуга автомойки.
/// Характеризуется названием, категорией автомобиля, стоимостью и длительностью в минутах.
/// </summary>
public class WashService
{
    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public required string Name { get; set; } = string.Empty;

    /// <summary>
    /// Категория автомобиля
    /// </summary>
    public required CarCategory Category { get; set; }

    /// <summary>
    /// Стоимость услуги в рублях
    /// </summary>
    public required decimal Cost { get; set; }

    /// <summary>
    /// Длительность услуги в минутах
    /// </summary>
    public required int DurationMinutes { get; set; }
    
    public override string ToString() => $"{Name} ({Category}) — {Cost} руб., {DurationMinutes} мин.";
}