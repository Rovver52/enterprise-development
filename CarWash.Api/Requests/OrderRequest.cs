using System.ComponentModel.DataAnnotations;

namespace CarWash.Api.Requests;

/// <summary>
/// Данные для создания или изменения заказа
/// </summary>
public class OrderRequest
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int CustomerId { get; set; }

    /// <summary>
    /// Идентификатор автомобиля
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int CarId { get; set; }

    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int WashServiceId { get; set; }

    /// <summary>
    /// Дата и время начала обслуживания
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Номер бокса
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int BoxNumber { get; set; }
}
