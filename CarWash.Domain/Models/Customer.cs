namespace CarWash.Domain.Models;

/// <summary>
/// Клиент автомойки
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
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Автомобили клиента
    /// </summary>
    public List<Car> Cars { get; set; } = [];
}