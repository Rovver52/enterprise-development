namespace CarWash.Domain.Models;

/// <summary>
/// Автомобиль клиента
/// Характеризуется госномером и маркой
/// </summary>
public class Car

{/// <summary>
/// Идентификатор автомобиля
/// </summary>
public required int Id { get; set; }

/// <summary>
/// Государственный номер автомобиля
/// </summary>
public required string LicensePlate { get; set; } = string.Empty;

/// <summary>
/// Марка автомобиля
/// </summary>
public required string Brand { get; set; } = string.Empty;

/// <summary>
/// Идентификатор клиента-владельца
/// </summary>
public int CustomerId { get; set; }

/// <summary>
/// Навигационное свойство: клиент-владелец
/// </summary>
public Customer? Customer { get; set; }

    /// <summary>
    /// Строковое представление автомобиля
    /// </summary>
    public override string ToString() => $"{Brand} [{LicensePlate}]";
}