namespace CarWash.Contracts.Responses;

/// <summary>
/// Сведения об автомобиле
/// </summary>
public class CarResponse
{
    /// <summary>
    /// Идентификатор
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

    /// <summary>
    /// Идентификатор владельца автомобиля
    /// </summary>
    public required int CustomerId { get; set; }
}
