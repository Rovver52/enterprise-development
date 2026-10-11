using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Domain.Services;

/// <summary>
/// Операции анализа услуг автомойки
/// </summary>
public class WashServiceAnalyticsService(
    IWashServiceRepository washServiceRepository,
    IOrderRepository orderRepository)
{
    /// <summary>
    /// Получить пять наиболее популярных услуг
    /// </summary>
    public IReadOnlyList<(WashService WashService, int OrderCount)> GetTop5PopularServices()
    {
        return washServiceRepository.GetAll()
            .Select(washService => (
                WashService: washService,
                OrderCount: orderRepository.GetAll()
                    .Count(order => order.WashService.Id == washService.Id)))
            .OrderByDescending(result => result.OrderCount)
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// Получить общую выручку по каждой услуге
    /// </summary>
    public IReadOnlyList<(WashService WashService, decimal Revenue)> GetRevenuePerService()
    {
        return washServiceRepository.GetAll()
            .Select(washService => (
                WashService: washService,
                Revenue: orderRepository.GetAll()
                    .Where(order => order.WashService.Id == washService.Id)
                    .Sum(order => order.WashService.Cost)))
            .ToList();
    }
}