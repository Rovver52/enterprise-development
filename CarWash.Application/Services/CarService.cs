using CarWash.Application.Mapping;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Операции приложения с автомобилями
/// </summary>
public class CarService(ICarRepository cars, ICustomerRepository customers, IOrderRepository orders)
{
    /// <summary>
    /// Получить все автомобили
    /// </summary>
    public IReadOnlyList<CarResponse> GetAll() => cars.GetAll().Select(car => car.ToResponse()).ToList();

    /// <summary>
    /// Получить автомобиль
    /// </summary>
    public CarResponse GetById(int id) =>
        (cars.GetById(id) ?? throw new KeyNotFoundException("Автомобиль не найден.")).ToResponse();

    /// <summary>
    /// Создать автомобиль
    /// </summary>
    public CarResponse Create(CarRequest request)
    {
        var car = request.ToDomain();
        cars.Add(car);
        return car.ToResponse();
    }

    /// <summary>
    /// Изменить автомобиль
    /// </summary>
    public void Update(int id, CarRequest request)
    {
        var car = cars.GetById(id) ?? throw new KeyNotFoundException("Автомобиль не найден.");
        request.ApplyTo(car);
        cars.Update(car);
    }

    /// <summary>
    /// Удалить автомобиль, который не используется в заказах
    /// </summary>
    public void Delete(int id)
    {
        _ = GetById(id);
        if (orders.GetByCarId(id).Count != 0)
            throw new InvalidOperationException("Автомобиль используется в заказах.");
        customers.RemoveCarFromAll(id);
        cars.Delete(id);
    }

    /// <summary>
    /// Получить клиентов автомобиля
    /// </summary>
    public IReadOnlyList<CustomerResponse> GetCustomers(int id)
    {
        _ = GetById(id);
        return customers.GetByCarId(id).Select(customer => customer.ToResponse()).ToList();
    }

    /// <summary>
    /// Получить заказы автомобиля
    /// </summary>
    public IReadOnlyList<OrderResponse> GetOrders(int id)
    {
        _ = GetById(id);
        return orders.GetByCarId(id).Select(order => order.ToResponse()).ToList();
    }
}
