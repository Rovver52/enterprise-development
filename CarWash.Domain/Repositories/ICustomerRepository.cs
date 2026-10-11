using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий клиентов
/// </summary>
public interface ICustomerRepository
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
    /// Обновить клиента
    /// </summary>
    void Update(Customer customer);

    /// <summary>
    /// Удалить клиента
    /// </summary>
    void Delete(int id);
}