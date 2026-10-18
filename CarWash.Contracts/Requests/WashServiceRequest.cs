using System.ComponentModel.DataAnnotations;

namespace CarWash.Contracts.Requests;

/// <summary>
/// Данные для создания или изменения услуги
/// </summary>
public class WashServiceRequest
{
    /// <summary>
    /// Название услуги
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Категория автомобиля
    /// </summary>
    [EnumDataType(typeof(CarCategoryDto))]
    public required CarCategoryDto Category { get; set; }

    /// <summary>
    /// Стоимость услуги
    /// </summary>
    [Range(0, double.MaxValue)]
    public required decimal Cost { get; set; }

    /// <summary>
    /// Продолжительность услуги в минутах
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int DurationMinutes { get; set; }
}
