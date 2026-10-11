using System.ComponentModel.DataAnnotations;
using CarWash.Domain.Models;

namespace CarWash.Api.Requests;

/// <summary>
/// Данные для создания или изменения услуги
/// </summary>
public class WashServiceRequest
{
    /// <summary>
    /// Название услуги
    /// </summary>
    [Required]
    public required string Name { get; set; }

    /// <summary>
    /// Категория автомобиля
    /// </summary>
    [EnumDataType(typeof(CarCategory))]
    public required CarCategory Category { get; set; }

    /// <summary>
    /// Стоимость услуги
    /// </summary>
    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public required decimal Cost { get; set; }

    /// <summary>
    /// Продолжительность услуги в минутах
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int DurationMinutes { get; set; }
}
