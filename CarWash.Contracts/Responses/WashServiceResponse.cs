namespace CarWash.Contracts.Responses;

/// <summary>
/// Сведения об услуге
/// </summary>
public class WashServiceResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Категория автомобиля
    /// </summary>
    public required CarCategoryDto Category { get; set; }

    /// <summary>
    /// Стоимость услуги
    /// </summary>
    public required decimal Cost { get; set; }

    /// <summary>
    /// Продолжительность услуги в минутах
    /// </summary>
    public required int DurationMinutes { get; set; }
}
