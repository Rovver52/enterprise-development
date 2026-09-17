using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Контекст заказов в памяти.
/// </summary>
public class InMemoryOrderContext : IOrderContext
{
    private readonly List<Order> _orders = new();

    public IReadOnlyList<Order> GetAll() => _orders.AsReadOnly();
    public Order? GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);
    public void Add(Order order) => _orders.Add(order);
    public void Update(Order order)
    {
        var index = _orders.FindIndex(o => o.Id == order.Id);
        if (index >= 0) _orders[index] = order;
    }
    public void Delete(int id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order != null) _orders.Remove(order);
    }
}