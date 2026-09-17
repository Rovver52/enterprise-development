using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Контекст клиентов в памяти.
/// </summary>
public class InMemoryCustomerContext : ICustomerContext
{
    private readonly List<Customer> _customers = new();

    public IReadOnlyList<Customer> GetAll() => _customers.AsReadOnly();
    public Customer? GetById(int id) => _customers.FirstOrDefault(c => c.Id == id);
    public void Add(Customer customer) => _customers.Add(customer);
    public void Update(Customer customer)
    {
        var index = _customers.FindIndex(c => c.Id == customer.Id);
        if (index >= 0) _customers[index] = customer;
    }
    public void Delete(int id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer != null) _customers.Remove(customer);
    }
}