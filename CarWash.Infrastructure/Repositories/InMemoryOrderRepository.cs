using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий заказов в памяти
/// </summary>
public class InMemoryOrderRepository(List<Order> orders)
    : IOrderRepository
{
    /// <summary>
    /// Получить все заказы
    /// </summary>
    public IReadOnlyList<Order> GetAll() =>
        orders.AsReadOnly();

    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    public Order? GetById(int id) =>
        orders.FirstOrDefault(order => order.Id == id);

    /// <summary>
    /// Добавить заказ
    /// </summary>
    public void Add(Order order) =>
        orders.Add(order);

    /// <summary>
    /// Обновить заказ
    /// </summary>
    public void Update(Order order)
    {
        var index = orders.FindIndex(
            item => item.Id == order.Id);

        if (index >= 0)
            orders[index] = order;
    }

    /// <summary>
    /// Удалить заказ
    /// </summary>
    public void Delete(int id) =>
        orders.RemoveAll(order => order.Id == id);
}