using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using CarWash.Domain.Services.Queries;

namespace CarWash.Domain.Services.QueryHandlers;

/// <summary>
/// Обработчик запроса топ-5 популярных услуг.
/// </summary>
public class PopularServicesQueryHandler : IPopularServicesQuery
{
    private readonly IOrderContext _orderContext;
    private readonly IWashServiceContext _serviceContext;

    public PopularServicesQueryHandler(IOrderContext orderContext, IWashServiceContext serviceContext)
    {
        _orderContext = orderContext;
        _serviceContext = serviceContext;
    }

    public List<(WashService Service, int OrderCount)> GetTop5PopularServices()
    {
        return _orderContext.GetAll()
            .GroupBy(o => o.ServiceId)
            .Select(g =>
            {
                var service = _serviceContext.GetById(g.Key);
                return service != null ? (service, g.Count()) : (null!, 0);
            })
            .Where(x => x.Item1 != null)
            .OrderByDescending(x => x.Item2)
            .Take(5)
            .ToList();
    }
}