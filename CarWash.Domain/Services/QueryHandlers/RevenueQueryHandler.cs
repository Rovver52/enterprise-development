using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using CarWash.Domain.Services.Queries;

namespace CarWash.Domain.Services.QueryHandlers;

/// <summary>
/// Обработчик запроса выручки по услугам.
/// </summary>
public class RevenueQueryHandler : IRevenueQuery
{
    private readonly IOrderContext _orderContext;
    private readonly IWashServiceContext _serviceContext;

    public RevenueQueryHandler(IOrderContext orderContext, IWashServiceContext serviceContext)
    {
        _orderContext = orderContext;
        _serviceContext = serviceContext;
    }

    public List<(WashService Service, decimal TotalRevenue)> GetRevenuePerService()
    {
        return _orderContext.GetAll()
            .GroupBy(o => o.ServiceId)
            .Select(g =>
            {
                var service = _serviceContext.GetById(g.Key);
                var revenue = service != null ? g.Sum(o => service.Cost) : 0;
                return (service!, revenue);
            })
            .Where(x => x.Item1 != null)
            .OrderByDescending(x => x.Item2)
            .ToList();
    }
}