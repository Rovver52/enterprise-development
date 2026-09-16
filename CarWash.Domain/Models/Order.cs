namespace CarWash.Domain.Models;

/// <summary>
/// Заказ на обслуживание автомобиля.
/// Содержит информацию о клиенте, услуге, автомобиле,
/// дате и времени начала обслуживания, номере бокса мойки.
/// Используется в качестве контракта.
/// </summary>
public class Order
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public Client? Client { get; set; }

    public int ServiceId { get; set; }
    public Service? Service { get; set; }

    public int CarId { get; set; }
    public Car? Car { get; set; }

    public DateTime StartTime { get; set; }
    public int BoxNumber { get; set; }

    public Order() { }

    public Order(int id, int clientId, int serviceId, int carId, DateTime startTime, int boxNumber)
    {
        Id = id;
        ClientId = clientId;
        ServiceId = serviceId;
        CarId = carId;
        StartTime = startTime;
        BoxNumber = boxNumber;
    }

    /// <summary>
    /// Время окончания обслуживания (вычисляется как StartTime + Duration).
    /// </summary>
    public DateTime EndTime => StartTime.AddMinutes(Service?.DurationMinutes ?? 0);

    /// <summary>
    /// Признак того, что автомобиль всё ещё находится на мойке.
    /// </summary>
    public bool IsInProgress(DateTime? now = null)
    {
        var currentTime = now ?? DateTime.Now;
        return currentTime >= StartTime && currentTime < EndTime;
    }

    public override string ToString() =>
        $"Заказ #{Id}: {Car} — {Service?.Name}, бокс {BoxNumber}, {StartTime:dd.MM.yyyy HH:mm}";
}