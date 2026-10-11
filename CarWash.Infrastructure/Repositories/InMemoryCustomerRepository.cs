using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий клиентов в памяти
/// </summary>
public class InMemoryCustomerRepository(List<Customer> customers)
    : ICustomerRepository
{
    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    public IReadOnlyList<Customer> GetAll() =>
        customers.AsReadOnly();

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    public Customer? GetById(int id) =>
        customers.FirstOrDefault(customer => customer.Id == id);

    /// <summary>
    /// Добавить клиента
    /// </summary>
    public void Add(Customer customer) =>
        customers.Add(customer);

    /// <summary>
    /// Обновить клиента
    /// </summary>
    public void Update(Customer customer)
    {
        var index = customers.FindIndex(
            item => item.Id == customer.Id);

        if (index >= 0)
            customers[index] = customer;
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    public void Delete(int id) =>
        customers.RemoveAll(customer => customer.Id == id);
}