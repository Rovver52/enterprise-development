using CarWash.Application.Mapping;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Операции приложения с услугами автомойки
/// </summary>
public class WashServiceService(IWashServiceRepository services, IOrderRepository orders)
{
    /// <summary>
    /// Получить все услуги
    /// </summary>
    public IReadOnlyList<WashServiceResponse> GetAll() =>
        services.GetAll().Select(service => service.ToResponse()).ToList();

    /// <summary>
    /// Получить услугу
    /// </summary>
    public WashServiceResponse GetById(int id) =>
        (services.GetById(id) ?? throw new KeyNotFoundException("Услуга не найдена.")).ToResponse();

    /// <summary>
    /// Создать услугу
    /// </summary>
    public WashServiceResponse Create(WashServiceRequest request)
    {
        var service = request.ToDomain();
        services.Add(service);
        return service.ToResponse();
    }

    /// <summary>
    /// Изменить услугу
    /// </summary>
    public void Update(int id, WashServiceRequest request)
    {
        var service = services.GetById(id) ?? throw new KeyNotFoundException("Услуга не найдена.");
        foreach (var order in orders.GetByWashServiceId(id))
            _ = order.StartTime.AddMinutes(request.DurationMinutes);
        request.ApplyTo(service);
        services.Update(service);
    }

    /// <summary>
    /// Удалить услугу, которая не используется в заказах
    /// </summary>
    public void Delete(int id)
    {
        _ = GetById(id);
        if (orders.GetByWashServiceId(id).Count != 0)
            throw new InvalidOperationException("Услуга используется в заказах.");
        services.Delete(id);
    }

    /// <summary>
    /// Получить заказы с услугой
    /// </summary>
    public IReadOnlyList<OrderResponse> GetOrders(int id)
    {
        _ = GetById(id);
        return orders.GetByWashServiceId(id).Select(order => order.ToResponse()).ToList();
    }
}
