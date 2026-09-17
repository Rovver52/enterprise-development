using CarWash.Domain.Repositories;
using CarWash.Domain.Services.Queries;

namespace CarWash.Domain.Services.QueryHandlers;

/// <summary>
/// Обработчик запроса доступности бокса.
/// </summary>
public class BoxAvailabilityQueryHandler : IBoxAvailabilityQuery
{
    private readonly IOrderContext _orderContext;

    public BoxAvailabilityQueryHandler(IOrderContext orderContext)
    {
        _orderContext = orderContext;
    }

    public DateTime? GetBoxFreeTime(int boxNumber, DateTime? now = null)
    {
        var currentTime = now ?? DateTime.Now;

        var activeOrders = _orderContext.GetAll()
            .Where(o => o.BoxNumber == boxNumber && o.EndTime > currentTime)
            .OrderBy(o => o.EndTime)
            .ToList();

        if (activeOrders.Count == 0)
            return null;

        return activeOrders.Last().EndTime;
    }
}