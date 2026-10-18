namespace CarWash.Contracts.Responses;

/// <summary>
/// Количество посещений клиента
/// </summary>
public class TopCustomerResponse
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int CustomerId { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Количество посещений
    /// </summary>
    public required int VisitCount { get; set; }
}
