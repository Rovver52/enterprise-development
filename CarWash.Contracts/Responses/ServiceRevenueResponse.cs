namespace CarWash.Contracts.Responses;

/// <summary>
/// Выручка по услуге
/// </summary>
public class ServiceRevenueResponse
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
    /// Суммарная выручка
    /// </summary>
    public required decimal Revenue { get; set; }
}
