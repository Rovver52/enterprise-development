namespace CarWash.Domain.Repositories;

using CarWash.Domain.Models;

/// <summary>
/// Контекст для работы с заказами
/// </summary>
public interface IOrderContext
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
    /// Обновить данные заказа
    /// </summary>
    void Update(Order order);

    /// <summary>
    /// Удалить заказ по идентификатору
    /// </summary>
    void Delete(int id);
}