using CarWash.Application.Mapping;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Операции приложения с клиентами
/// </summary>
public class CustomerService(ICustomerRepository customers, ICarRepository cars, IOrderRepository orders)
{
    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    public IReadOnlyList<CustomerResponse> GetAll() =>
        customers.GetAll().Select(customer => customer.ToResponse()).ToList();

    /// <summary>
    /// Получить клиента
    /// </summary>
    public CustomerResponse GetById(int id) =>
        (customers.GetById(id) ?? throw new KeyNotFoundException("Клиент не найден.")).ToResponse();

    /// <summary>
    /// Создать клиента
    /// </summary>
    public CustomerResponse Create(CustomerRequest request)
    {
        var customer = request.ToDomain();
        customers.Add(customer);
        return customer.ToResponse();
    }

    /// <summary>
    /// Изменить собственные поля клиента
    /// </summary>
    public void Update(int id, CustomerRequest request)
    {
        var customer = customers.GetById(id) ?? throw new KeyNotFoundException("Клиент не найден.");
        request.ApplyTo(customer);
        customers.Update(customer);
    }

    /// <summary>
    /// Удалить клиента, который не используется в заказах
    /// </summary>
    public void Delete(int id)
    {
        _ = GetById(id);
        if (orders.GetByCustomerId(id).Count != 0)
            throw new InvalidOperationException("Клиент используется в заказах.");
        customers.Delete(id);
    }

    /// <summary>
    /// Получить автомобили клиента
    /// </summary>
    public IReadOnlyList<CarResponse> GetCars(int id)
    {
        _ = GetById(id);
        return customers.GetCars(id).Select(car => car.ToResponse()).ToList();
    }

    /// <summary>
    /// Получить заказы клиента
    /// </summary>
    public IReadOnlyList<OrderResponse> GetOrders(int id)
    {
        _ = GetById(id);
        return orders.GetByCustomerId(id).Select(order => order.ToResponse()).ToList();
    }

    /// <summary>
    /// Связать клиента с существующим автомобилем
    /// </summary>
    public void AddCar(int id, int carId)
    {
        _ = GetById(id);
        var car = cars.GetById(carId) ?? throw new KeyNotFoundException("Автомобиль не найден.");
        customers.AddCar(id, car);
    }

    /// <summary>
    /// Удалить связь клиента с автомобилем
    /// </summary>
    public void RemoveCar(int id, int carId)
    {
        _ = GetById(id);
        if (cars.GetById(carId) is null)
            throw new KeyNotFoundException("Автомобиль не найден.");
        customers.RemoveCar(id, carId);
    }
}
