using CarWash.Domain.Services.Queries;
using CarWash.Domain.Models;

namespace CarWash.Domain.Services;

/// <summary>
/// Сервис бизнес-логики автомойки.
/// Объединяет все запросы бизнес-логики.
/// </summary>
public class CarWashService(
    ITopClientsQuery topClientsQuery,
    IPopularServicesQuery popularServicesQuery,
    IRevenueQuery revenueQuery,
    IActiveCarsQuery activeCarsQuery,
    IBoxAvailabilityQuery boxAvailabilityQuery)
{
    private readonly ITopClientsQuery _topClientsQuery = topClientsQuery;
    private readonly IPopularServicesQuery _popularServicesQuery = popularServicesQuery;
    private readonly IRevenueQuery _revenueQuery = revenueQuery;
    private readonly IActiveCarsQuery _activeCarsQuery = activeCarsQuery;
    private readonly IBoxAvailabilityQuery _boxAvailabilityQuery = boxAvailabilityQuery;

    /// <summary>
    /// 1. Топ-5 клиентов по количеству посещений (заказов).
    /// </summary>
    public List<(Customer Client, int VisitCount)> GetTop5ClientsByVisits() =>
        _topClientsQuery.GetTop5ClientsByVisits();

    /// <summary>
    /// 2. Автомобили, находящиеся на мойке в данный момент.
    /// </summary>
    public List<Car> GetCarsCurrentlyAtWash() =>
        _activeCarsQuery.GetCarsCurrentlyAtWash();

    /// <summary>
    /// 3. Топ-5 наиболее популярных услуг.
    /// </summary>
    public List<(WashService Service, int OrderCount)> GetTop5PopularServices() =>
        _popularServicesQuery.GetTop5PopularServices();

    /// <summary>
    /// 4. Для выбранного бокса мойки — время, когда он освободится.
    /// Возвращает null, если бокс свободен.
    /// </summary>
    public DateTime? GetBoxFreeTime(int boxNumber, DateTime? now = null) =>
        _boxAvailabilityQuery.GetBoxFreeTime(boxNumber, now);

    /// <summary>
    /// 5. Суммарная выручка по каждой услуге.
    /// </summary>
    public List<(WashService Service, decimal TotalRevenue)> GetRevenuePerService() =>
        _revenueQuery.GetRevenuePerService();
}