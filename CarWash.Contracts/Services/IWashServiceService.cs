using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;

namespace CarWash.Contracts.Services;

/// <summary>
/// Операции с услугами автомойки
/// </summary>
public interface IWashServiceService : ICrudService<WashServiceRequest, WashServiceResponse>
{
    /// <summary>
    /// Получить заказы с услугой
    /// </summary>
    IReadOnlyList<OrderResponse> GetOrders(int id);
}
