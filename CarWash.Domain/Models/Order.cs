namespace CarWash.Domain.Models;

/// <summary>
/// Заказ на обслуживание автомобиля.
/// Содержит информацию о клиенте, услуге, автомобиле,
/// дате и времени начала обслуживания, номере бокса мойки.
/// Используется в качестве контракта.
/// </summary>
public class Order
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public required int ServiceId { get; set; }

    /// <summary>
    /// Навигационное свойство: услуга
    /// </summary>
    public WashService? Service { get; set; }

    /// <summary>
    /// Идентификатор автомобиля
    /// </summary>
    public required int CarId { get; set; }

    /// <summary>
    /// Навигационное свойство: автомобиль
    /// </summary>
    public Car? Car { get; set; }

    /// <summary>
    /// Дата и время начала обслуживания
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Номер бокса мойки
    /// </summary>
    public required int BoxNumber { get; set; }

    /// <summary>
    /// Время окончания обслуживания (вычисляется как StartTime + Duration)
    /// </summary>
    public DateTime EndTime => StartTime.AddMinutes(Service?.DurationMinutes ?? 0);

    /// <summary>
    /// Метод, который показывает, что автомобиль всё ещё находится на мойке
    /// </summary>
public bool IsInProgress => DateTime.Now>= StartTime && DateTime.Now< EndTime;

    public override string ToString() =>
        $"Заказ #{Id}: {Car} — {Service?.Name}, бокс {BoxNumber}, {StartTime:dd.MM.yyyy HH:mm}";
}