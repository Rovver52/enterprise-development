using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;

namespace CarWash.Contracts.Services;

/// <summary>
/// Операции с заказами
/// </summary>
public interface IOrderService : ICrudService<OrderRequest, OrderResponse>
{
    /// <summary>
    /// Получить клиента заказа
    /// </summary>
    CustomerResponse GetCustomer(int id);

    /// <summary>
    /// Получить автомобиль заказа
    /// </summary>
    CarResponse GetCar(int id);

    /// <summary>
    /// Получить услугу заказа
    /// </summary>
    WashServiceResponse GetWashService(int id);
}
