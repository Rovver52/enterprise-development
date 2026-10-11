namespace CarWash.Domain.Models;

/// <summary>
/// Услуга автомойки
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
    public required string Name { get; set; }

    /// <summary>
    /// Категория автомобиля
    /// </summary>
    public required CarCategory Category { get; set; }

    /// <summary>
    /// Стоимость услуги
    /// </summary>
    public required decimal Cost { get; set; }

    /// <summary>
    /// Продолжительность услуги в минутах
    /// </summary>
    public required int DurationMinutes { get; set; }
}