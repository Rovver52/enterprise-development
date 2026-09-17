namespace CarWash.Domain.Repositories;

using CarWash.Domain.Models;

/// <summary>
/// Контекст для работы с клиентами
/// </summary>
public interface ICustomerContext
{
    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    IReadOnlyList<Customer> GetAll();

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    Customer? GetById(int id);

    /// <summary>
    /// Добавить клиента
    /// </summary>
    void Add(Customer customer);

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    void Update(Customer customer);

    /// <summary>
    /// Удалить клиента по идентификатору
    /// </summary>
    void Delete(int id);
}