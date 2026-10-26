using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;

namespace CarWash.Contracts.Services;

/// <summary>
/// Операции с клиентами
/// </summary>
public interface ICustomerService : ICrudService<CustomerRequest, CustomerResponse>
{
    /// <summary>
    /// Получить автомобили клиента
    /// </summary>
    IReadOnlyList<CarResponse> GetCars(int id);

    /// <summary>
    /// Получить заказы клиента
    /// </summary>
    IReadOnlyList<OrderResponse> GetOrders(int id);
}
