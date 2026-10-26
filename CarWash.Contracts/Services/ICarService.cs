using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;

namespace CarWash.Contracts.Services;

/// <summary>
/// Операции с автомобилями
/// </summary>
public interface ICarService : ICrudService<CarRequest, CarResponse>
{
    /// <summary>
    /// Получить владельца автомобиля
    /// </summary>
    CustomerResponse GetCustomer(int id);

    /// <summary>
    /// Получить заказы автомобиля
    /// </summary>
    IReadOnlyList<OrderResponse> GetOrders(int id);
}
