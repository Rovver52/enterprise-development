namespace CarWash.Contracts.Responses;

/// <summary>
/// Сведения о клиенте
/// </summary>
public class CustomerResponse
{
    /// <summary>
    /// Идентификатор
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
}
