using CarWash.Application.Exceptions;
using CarWash.Application.Mapping;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Операции приложения с заказами
/// </summary>
public class OrderService(
    IOrderRepository orders,
    ICustomerRepository customers,
    ICarRepository cars,
    IWashServiceRepository services) : IOrderService
{
    /// <inheritdoc />
    public IReadOnlyList<OrderResponse> GetAll() => orders.GetAll().Select(order => order.ToResponse()).ToList();

    /// <inheritdoc />
    public OrderResponse GetById(int id) =>
        (orders.GetById(id) ?? throw new EntityNotFoundException("Заказ не найден.")).ToResponse();

    /// <inheritdoc />
    public OrderResponse Create(OrderRequest request)
    {
        var order = CreateOrder(request);
        orders.Add(order);
        return order.ToResponse();
    }

    /// <inheritdoc />
    public void Update(int id, OrderRequest request)
    {
        _ = GetById(id);
        var order = CreateOrder(request);
        order.Id = id;
        orders.Update(order);
    }

    /// <inheritdoc />
    public void Delete(int id)
    {
        _ = GetById(id);
        orders.Delete(id);
    }

    /// <inheritdoc />
    public CustomerResponse GetCustomer(int id) =>
        (orders.GetCustomer(id) ?? throw new EntityNotFoundException("Заказ не найден.")).ToResponse();

    /// <inheritdoc />
    public CarResponse GetCar(int id) =>
        (orders.GetCar(id) ?? throw new EntityNotFoundException("Заказ не найден.")).ToResponse();

    /// <inheritdoc />
    public WashServiceResponse GetWashService(int id) =>
        (orders.GetWashService(id) ?? throw new EntityNotFoundException("Заказ не найден.")).ToResponse();

    private Order CreateOrder(OrderRequest request)
    {
        var customer = customers.GetById(request.CustomerId)
            ?? throw new EntityNotFoundException("Клиент не найден.");
        var car = cars.GetById(request.CarId)
            ?? throw new EntityNotFoundException("Автомобиль не найден.");
        var service = services.GetById(request.WashServiceId)
            ?? throw new EntityNotFoundException("Услуга не найдена.");
        var order = request.ToDomain(customer, car, service);
        try
        {
            _ = order.EndTime;
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new RequestValidationException(nameof(request.StartTime),
                "Время окончания заказа выходит за допустимые границы даты.");
        }
        return order;
    }
}
