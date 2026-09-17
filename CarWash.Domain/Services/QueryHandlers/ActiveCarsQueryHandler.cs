using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using CarWash.Domain.Services.Queries;

namespace CarWash.Domain.Services.QueryHandlers;

/// <summary>
/// Обработчик запроса активных автомобилей.
/// </summary>
public class ActiveCarsQueryHandler : IActiveCarsQuery
{
    private readonly IOrderContext _orderContext;
    private readonly ICarContext _carContext;

    public ActiveCarsQueryHandler(IOrderContext orderContext, ICarContext carContext)
    {
        _orderContext = orderContext;
        _carContext = carContext;
    }

    public List<Car> GetCarsCurrentlyAtWash()
    {
        return _orderContext.GetAll()
            .Where(o => o.IsInProgress)
            .Select(o => _carContext.GetById(o.CarId))
            .Where(c => c != null)
            .Distinct()
            .ToList()!;
    }
}