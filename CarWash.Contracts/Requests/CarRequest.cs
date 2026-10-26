using System.ComponentModel.DataAnnotations;

namespace CarWash.Contracts.Requests;

/// <summary>
/// Данные для создания или изменения автомобиля
/// </summary>
public class CarRequest
{
    /// <summary>
    /// Государственный номер
    /// </summary>
    [Required]
    public string LicensePlate { get; set; } = string.Empty;

    /// <summary>
    /// Марка автомобиля
    /// </summary>
    [Required]
    public string Brand { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор владельца автомобиля
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Идентификатор владельца должен быть положительным.")]
    public required int CustomerId { get; set; }
}
