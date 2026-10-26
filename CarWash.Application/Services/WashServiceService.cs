using CarWash.Application.Exceptions;
using CarWash.Application.Mapping;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Операции приложения с услугами автомойки
/// </summary>
public class WashServiceService(IWashServiceRepository services, IOrderRepository orders) : IWashServiceService
{
    /// <inheritdoc />
    public IReadOnlyList<WashServiceResponse> GetAll() =>
        services.GetAll().Select(service => service.ToResponse()).ToList();

    /// <inheritdoc />
    public WashServiceResponse GetById(int id) =>
        (services.GetById(id) ?? throw new EntityNotFoundException("Услуга не найдена.")).ToResponse();

    /// <inheritdoc />
    public WashServiceResponse Create(WashServiceRequest request)
    {
        var service = request.ToDomain();
        services.Add(service);
        return service.ToResponse();
    }

    /// <inheritdoc />
    public void Update(int id, WashServiceRequest request)
    {
        var service = services.GetById(id) ?? throw new EntityNotFoundException("Услуга не найдена.");
        foreach (var order in orders.GetByWashServiceId(id))
        {
            try
            {
                _ = order.StartTime.AddMinutes(request.DurationMinutes);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new RequestValidationException(nameof(request.DurationMinutes),
                    "Продолжительность услуги выводит время окончания существующего заказа за допустимые границы даты.");
            }
        }
        request.ApplyTo(service);
        services.Update(service);
    }

    /// <inheritdoc />
    public void Delete(int id)
    {
        _ = GetById(id);
        if (orders.GetByWashServiceId(id).Count != 0)
            throw new EntityConflictException("Услуга используется в заказах.");
        services.Delete(id);
    }

    /// <inheritdoc />
    public IReadOnlyList<OrderResponse> GetOrders(int id)
    {
        _ = GetById(id);
        return orders.GetByWashServiceId(id).Select(order => order.ToResponse()).ToList();
    }
}
