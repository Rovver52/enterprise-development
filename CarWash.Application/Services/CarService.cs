using CarWash.Application.Exceptions;
using CarWash.Application.Mapping;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Операции приложения с автомобилями
/// </summary>
public class CarService(ICarRepository cars, ICustomerRepository customers, IOrderRepository orders) : ICarService
{
    /// <inheritdoc />
    public IReadOnlyList<CarResponse> GetAll() => cars.GetAll().Select(car => car.ToResponse()).ToList();

    /// <inheritdoc />
    public CarResponse GetById(int id) =>
        (cars.GetById(id) ?? throw new EntityNotFoundException("Автомобиль не найден.")).ToResponse();

    /// <inheritdoc />
    public CarResponse Create(CarRequest request)
    {
        var customer = customers.GetById(request.CustomerId)
            ?? throw new EntityNotFoundException("Владелец автомобиля не найден.");
        var car = request.ToDomain(customer);
        cars.Add(car);
        return car.ToResponse();
    }

    /// <inheritdoc />
    public void Update(int id, CarRequest request)
    {
        _ = GetById(id);
        var customer = customers.GetById(request.CustomerId)
            ?? throw new EntityNotFoundException("Владелец автомобиля не найден.");
        var car = request.ToDomain(customer);
        car.Id = id;
        cars.Update(car);
    }

    /// <inheritdoc />
    public void Delete(int id)
    {
        _ = GetById(id);
        if (orders.GetByCarId(id).Count != 0)
            throw new EntityConflictException("Автомобиль используется в заказах.");
        cars.Delete(id);
    }

    /// <inheritdoc />
    public CustomerResponse GetCustomer(int id) =>
        (cars.GetCustomer(id) ?? throw new EntityNotFoundException("Автомобиль не найден.")).ToResponse();

    /// <inheritdoc />
    public IReadOnlyList<OrderResponse> GetOrders(int id)
    {
        _ = GetById(id);
        return orders.GetByCarId(id).Select(order => order.ToResponse()).ToList();
    }
}
