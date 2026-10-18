using System.ComponentModel.DataAnnotations;

namespace CarWash.Contracts.Requests;

/// <summary>
/// Данные для создания или изменения клиента
/// </summary>
public class CustomerRequest
{
    /// <summary>
    /// ФИО клиента
    /// </summary>
    [Required]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Required]
    public string Phone { get; set; } = string.Empty;
}
