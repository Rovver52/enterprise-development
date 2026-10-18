namespace CarWash.Contracts.Responses;

/// <summary>
/// Количество заказов услуги
/// </summary>
public class PopularServiceResponse
{
    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public required int WashServiceId { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Количество заказов
    /// </summary>
    public required int OrderCount { get; set; }
}
