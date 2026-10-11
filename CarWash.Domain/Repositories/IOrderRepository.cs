using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий заказов
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Получить все заказы
    /// </summary>
    IReadOnlyList<Order> GetAll();

    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    Order? GetById(int id);

    /// <summary>
    /// Добавить заказ
    /// </summary>
    void Add(Order order);

    /// <summary>
    /// Обновить заказ
    /// </summary>
    void Update(Order order);

    /// <summary>
    /// Удалить заказ
    /// </summary>
    void Delete(int id);
}