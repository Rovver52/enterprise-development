namespace CarWash.Contracts.Responses;

/// <summary>
/// Сведения о заказе без вложенных сущностей
/// </summary>
public class OrderResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int CustomerId { get; set; }

    /// <summary>
    /// Идентификатор автомобиля
    /// </summary>
    public required int CarId { get; set; }

    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public required int WashServiceId { get; set; }

    /// <summary>
    /// Начало обслуживания
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Окончание обслуживания
    /// </summary>
    public required DateTime EndTime { get; set; }

    /// <summary>
    /// Номер бокса
    /// </summary>
    public required int BoxNumber { get; set; }

    /// <summary>
    /// Автомобиль находится на мойке
    /// </summary>
    public required bool IsInProgress { get; set; }
}
