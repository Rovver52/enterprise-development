using CarWash.Domain.Data;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий услуг и их аналитики в памяти
/// </summary>
public class InMemoryWashServiceRepository(CarWashData data, IOrderRepository orderRepository)
    : InMemoryRepository<WashService>(data.WashServices), IWashServiceRepository
{
    /// <inheritdoc />
    public IReadOnlyList<(WashService WashService, int OrderCount)> GetTop5PopularServices()
    {
        var counts = orderRepository.GetAll()
            .GroupBy(order => order.WashService.Id)
            .ToDictionary(group => group.Key, group => group.Count());
        return Items.Values
            .Select(service => (WashService: service, OrderCount: counts.GetValueOrDefault(service.Id)))
            .OrderByDescending(item => item.OrderCount).ThenBy(item => item.WashService.Id)
            .Take(5).ToList();
    }

    /// <inheritdoc />
    public IReadOnlyList<(WashService WashService, decimal Revenue)> GetRevenuePerService()
    {
        var revenue = orderRepository.GetAll()
            .GroupBy(order => order.WashService.Id)
            .ToDictionary(group => group.Key, group => group.Sum(order => order.WashService.Cost));
        return Items.Values.OrderBy(service => service.Id)
            .Select(service => (WashService: service, Revenue: revenue.GetValueOrDefault(service.Id)))
            .ToList();
    }
}
