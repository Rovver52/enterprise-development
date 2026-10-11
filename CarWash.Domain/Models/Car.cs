namespace CarWash.Domain.Models;

/// <summary>
/// Автомобиль клиента
/// </summary>
public class Car
{
    /// <summary>
    /// Идентификатор автомобиля
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Государственный номер
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка автомобиля
    /// </summary>
    public required string Brand { get; set; }
}