using CarWash.Application.Mapping;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
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
    IWashServiceRepository services)
{
    /// <summary>
    /// Получить все заказы
    /// </summary>
    public IReadOnlyList<OrderResponse> GetAll() => orders.GetAll().Select(order => order.ToResponse()).ToList();

    /// <summary>
    /// Получить заказ
    /// </summary>
    public OrderResponse GetById(int id) =>
        (orders.GetById(id) ?? throw new KeyNotFoundException("Заказ не найден.")).ToResponse();

    /// <summary>
    /// Создать заказ
    /// </summary>
    public OrderResponse Create(OrderRequest request)
    {
        var order = CreateOrder(request);
        orders.Add(order);
        return order.ToResponse();
    }

    /// <summary>
    /// Изменить заказ
    /// </summary>
    public void Update(int id, OrderRequest request)
    {
        _ = GetById(id);
        var order = CreateOrder(request);
        order.Id = id;
        orders.Update(order);
    }

    /// <summary>
    /// Удалить заказ
    /// </summary>
    public void Delete(int id)
    {
        _ = GetById(id);
        orders.Delete(id);
    }

    /// <summary>
    /// Получить клиента заказа
    /// </summary>
    public CustomerResponse GetCustomer(int id) =>
        (orders.GetCustomer(id) ?? throw new KeyNotFoundException("Заказ не найден.")).ToResponse();

    /// <summary>
    /// Получить автомобиль заказа
    /// </summary>
    public CarResponse GetCar(int id) =>
        (orders.GetCar(id) ?? throw new KeyNotFoundException("Заказ не найден.")).ToResponse();

    /// <summary>
    /// Получить услугу заказа
    /// </summary>
    public WashServiceResponse GetWashService(int id) =>
        (orders.GetWashService(id) ?? throw new KeyNotFoundException("Заказ не найден.")).ToResponse();

    private Order CreateOrder(OrderRequest request)
    {
        var customer = customers.GetById(request.CustomerId)
            ?? throw new ArgumentException("Клиент не найден.", nameof(request.CustomerId));
        var car = cars.GetById(request.CarId)
            ?? throw new ArgumentException("Автомобиль не найден.", nameof(request.CarId));
        var service = services.GetById(request.WashServiceId)
            ?? throw new ArgumentException("Услуга не найдена.", nameof(request.WashServiceId));
        var order = request.ToDomain(customer, car, service);
        _ = order.EndTime;
        return order;
    }
}
