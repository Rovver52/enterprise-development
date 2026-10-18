using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий заказов
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    /// <summary>
    /// Получить заказы клиента
    /// </summary>
    IReadOnlyList<Order> GetByCustomerId(int customerId);

    /// <summary>
    /// Получить заказы автомобиля
    /// </summary>
    IReadOnlyList<Order> GetByCarId(int carId);

    /// <summary>
    /// Получить заказы с указанной услугой
    /// </summary>
    IReadOnlyList<Order> GetByWashServiceId(int washServiceId);

    /// <summary>
    /// Получить клиента заказа
    /// </summary>
    Customer? GetCustomer(int orderId);

    /// <summary>
    /// Получить автомобиль заказа
    /// </summary>
    Car? GetCar(int orderId);

    /// <summary>
    /// Получить услугу заказа
    /// </summary>
    WashService? GetWashService(int orderId);

    /// <summary>
    /// Получить автомобили, которые сейчас находятся на мойке
    /// </summary>
    IReadOnlyList<Car> GetCarsCurrentlyAtWash();

    /// <summary>
    /// Определить время освобождения бокса
    /// </summary>
    DateTime GetBoxAvailableFrom(int boxNumber);
}
