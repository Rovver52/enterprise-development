using System.ComponentModel.DataAnnotations;

namespace CarWash.Api.Requests;

/// <summary>
/// Данные для создания или изменения клиента
/// </summary>
public class CustomerRequest
{
    /// <summary>
    /// ФИО клиента
    /// </summary>
    [Required]
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Required]
    public required string Phone { get; set; }

    /// <summary>
    /// Идентификаторы уже созданных автомобилей клиента
    /// </summary>
    [Required]
    public List<int> CarIds { get; set; } = [];
}
