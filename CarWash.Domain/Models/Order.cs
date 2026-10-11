namespace CarWash.Domain.Models;

/// <summary>
/// Заказ на обслуживание автомобиля
/// </summary>
public class Order
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required Customer Customer { get; set; }

    /// <summary>
    /// Услуга
    /// </summary>
    public required WashService WashService { get; set; }

    /// <summary>
    /// Автомобиль
    /// </summary>
    public required Car Car { get; set; }

    /// <summary>
    /// Дата и время начала обслуживания
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Номер бокса
    /// </summary>
    public required int BoxNumber { get; set; }

    /// <summary>
    /// Дата и время окончания обслуживания
    /// </summary>
    public DateTime EndTime =>
        StartTime.AddMinutes(WashService.DurationMinutes);

    /// <summary>
    /// Автомобиль находится на мойке
    /// </summary>
    public bool IsInProgress =>
        DateTime.Now >= StartTime && DateTime.Now < EndTime;
}