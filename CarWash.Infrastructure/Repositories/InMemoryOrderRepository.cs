using CarWash.Domain.Data;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий заказов и аналитических запросов в памяти
/// </summary>
public class InMemoryOrderRepository(CarWashData data)
    : InMemoryRepository<Order>(data.Orders), IOrderRepository
{
    /// <inheritdoc />
    public IReadOnlyList<Order> GetByCustomerId(int customerId) =>
        Items.Values.Where(order => order.Customer.Id == customerId)
            .OrderBy(order => order.Id).ToList();

    /// <inheritdoc />
    public IReadOnlyList<Order> GetByCarId(int carId) =>
        Items.Values.Where(order => order.Car.Id == carId)
            .OrderBy(order => order.Id).ToList();

    /// <inheritdoc />
    public IReadOnlyList<Order> GetByWashServiceId(int washServiceId) =>
        Items.Values.Where(order => order.WashService.Id == washServiceId)
            .OrderBy(order => order.Id).ToList();

    /// <inheritdoc />
    public Customer? GetCustomer(int orderId) => GetById(orderId)?.Customer;

    /// <inheritdoc />
    public Car? GetCar(int orderId) => GetById(orderId)?.Car;

    /// <inheritdoc />
    public WashService? GetWashService(int orderId) => GetById(orderId)?.WashService;

    /// <inheritdoc />
    public IReadOnlyList<Car> GetCarsCurrentlyAtWash() =>
        Items.Values.Where(order => order.IsInProgress)
            .Select(order => order.Car).DistinctBy(car => car.Id)
            .OrderBy(car => car.Id).ToList();

    /// <inheritdoc />
    public DateTime GetBoxAvailableFrom(int boxNumber)
    {
        var currentOrder = Items.Values
            .Where(order => order.BoxNumber == boxNumber && order.IsInProgress)
            .OrderBy(order => order.EndTime)
            .FirstOrDefault();
        return currentOrder?.EndTime ?? DateTime.Now;
    }
}
