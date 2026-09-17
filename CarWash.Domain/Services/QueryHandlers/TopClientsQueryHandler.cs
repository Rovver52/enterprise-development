using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using CarWash.Domain.Services.Queries;

namespace CarWash.Domain.Services.QueryHandlers;

/// <summary>
/// Обработчик запроса топ-5 клиентов.
/// </summary>
public class TopClientsQueryHandler : ITopClientsQuery
{
    private readonly IOrderContext _orderContext;
    private readonly ICustomerContext _customerContext;

    public TopClientsQueryHandler(IOrderContext orderContext, ICustomerContext customerContext)
    {
        _orderContext = orderContext;
        _customerContext = customerContext;
    }

    public List<(Customer Client, int VisitCount)> GetTop5ClientsByVisits()
    {
        return _orderContext.GetAll()
            .GroupBy(o => o.CustomerId)
            .Select(g =>
            {
                var customer = _customerContext.GetById(g.Key);
                return customer != null ? (customer, g.Count()) : (null!, 0);
            })
            .Where(x => x.Item1 != null)
            .OrderByDescending(x => x.Item2)
            .Take(5)
            .ToList();
    }
}