namespace CarWash.Domain.Models;

/// <summary>
/// Клиент автомойки.
/// Характеризуется ФИО и телефоном. Может иметь несколько автомобилей
/// </summary>
public class Customer
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Телефон клиента
    /// </summary>
    public required string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Список автомобилей клиента
    /// </summary>
    public List<Car> Cars { get; set; } = [];

    public override string ToString() => $"{FullName} ({Phone})";
}