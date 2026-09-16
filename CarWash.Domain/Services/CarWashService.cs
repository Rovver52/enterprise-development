using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Domain.Services;

/// <summary>
/// Сервис бизнес-логики автомойки.
/// Реализует все запросы из задания на unit-тесты.
/// </summary>
public class CarWashService
{
    private readonly IOrderRepository _repository;

    public CarWashService(IOrderRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 1. Топ-5 клиентов по количеству посещений (заказов).
    /// </summary>
    public List<(Client Client, int VisitCount)> GetTop5ClientsByVisits()
    {
        return _repository.GetAllOrders()
            .GroupBy(o => o.ClientId)
            .Select(g => new
            {
                Client = _repository.GetAllClients().First(c => c.Id == g.Key),
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Select(x => (x.Client, x.Count))
            .ToList();
    }

    /// <summary>
    /// 2. Автомобили, находящиеся на мойке в данный момент.
    /// </summary>
    public List<Car> GetCarsCurrentlyAtWash(DateTime? now = null)
    {
        var currentTime = now ?? DateTime.Now;
        return _repository.GetAllOrders()
            .Where(o => o.IsInProgress(currentTime))
            .Select(o => o.Car!)
            .Where(c => c != null)
            .Distinct()
            .ToList();
    }

    /// <summary>
    /// 3. Топ-5 наиболее популярных услуг.
    /// </summary>
    public List<(Service Service, int OrderCount)> GetTop5PopularServices()
    {
        return _repository.GetAllOrders()
            .GroupBy(o => o.ServiceId)
            .Select(g => new
            {
                Service = _repository.GetAllServices().First(s => s.Id == g.Key),
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Select(x => (x.Service, x.Count))
            .ToList();
    }

    /// <summary>
    /// 4. Для выбранного бокса мойки — время, когда он освободится.
    /// Возвращает null, если бокс свободен.
    /// </summary>
    public DateTime? GetBoxFreeTime(int boxNumber, DateTime? now = null)
    {
        var currentTime = now ?? DateTime.Now;

        var activeOrders = _repository.GetAllOrders()
            .Where(o => o.BoxNumber == boxNumber && o.EndTime > currentTime)
            .OrderBy(o => o.EndTime)
            .ToList();

        if (activeOrders.Count == 0)
            return null;

        // Бокс освободится после окончания последнего активного заказа
        return activeOrders.Last().EndTime;
    }

    /// <summary>
    /// 5. Суммарная выручка по каждой услуге.
    /// </summary>
    public List<(Service Service, decimal TotalRevenue)> GetRevenuePerService()
    {
        return _repository.GetAllOrders()
            .GroupBy(o => o.ServiceId)
            .Select(g => new
            {
                Service = _repository.GetAllServices().First(s => s.Id == g.Key),
                Revenue = g.Sum(o => o.Service!.Cost)
            })
            .OrderByDescending(x => x.Revenue)
            .Select(x => (x.Service, x.Revenue))
            .ToList();
    }
}