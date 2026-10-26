using System.Text.Json.Serialization;

namespace CarWash.Contracts;

/// <summary>
/// Категория автомобиля
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<CarCategoryDto>))]
public enum CarCategoryDto
{
    /// <summary>
    /// Легковой автомобиль
    /// </summary>
    Sedan,

    /// <summary>
    /// Грузовой автомобиль
    /// </summary>
    Truck,

    /// <summary>
    /// Микроавтобус
    /// </summary>
    Minibus,

    /// <summary>
    /// Внедорожник
    /// </summary>
    SUV,

    /// <summary>
    /// Мотоцикл
    /// </summary>
    Motorcycle
}
