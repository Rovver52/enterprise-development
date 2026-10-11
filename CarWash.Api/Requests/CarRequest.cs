using System.ComponentModel.DataAnnotations;

namespace CarWash.Api.Requests;

/// <summary>
/// Данные для создания или изменения автомобиля
/// </summary>
public class CarRequest
{
    /// <summary>
    /// Государственный номер
    /// </summary>
    [Required]
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка автомобиля
    /// </summary>
    [Required]
    public required string Brand { get; set; }
}
