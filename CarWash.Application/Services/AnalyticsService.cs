using CarWash.Application.Mapping;
using CarWash.Contracts.Responses;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Подготовка ответов аналитических запросов
/// </summary>
public class AnalyticsService(
    ICustomerRepository customers,
    IOrderRepository orders,
    IWashServiceRepository services)
{
    /// <summary>
    /// Получить пять клиентов с наибольшим количеством посещений
    /// </summary>
    public IReadOnlyList<TopCustomerResponse> GetTopCustomers() =>
        customers.GetTop5CustomersByVisits().Select(item => new TopCustomerResponse
        {
            CustomerId = item.Customer.Id,
            FullName = item.Customer.FullName,
            VisitCount = item.VisitCount
        }).ToList();

    /// <summary>
    /// Получить автомобили, которые сейчас находятся на мойке
    /// </summary>
    public IReadOnlyList<CarResponse> GetCurrentCars() =>
        orders.GetCarsCurrentlyAtWash().Select(car => car.ToResponse()).ToList();

    /// <summary>
    /// Получить пять наиболее популярных услуг
    /// </summary>
    public IReadOnlyList<PopularServiceResponse> GetTopServices() =>
        services.GetTop5PopularServices().Select(item => new PopularServiceResponse
        {
            WashServiceId = item.WashService.Id,
            Name = item.WashService.Name,
            OrderCount = item.OrderCount
        }).ToList();

    /// <summary>
    /// Получить время освобождения бокса
    /// </summary>
    public BoxAvailabilityResponse GetBoxAvailability(int boxNumber)
    {
        if (boxNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(boxNumber), "Номер бокса должен быть положительным.");
        return new BoxAvailabilityResponse
        {
            BoxNumber = boxNumber,
            AvailableFrom = orders.GetBoxAvailableFrom(boxNumber)
        };
    }

    /// <summary>
    /// Получить выручку по каждой услуге
    /// </summary>
    public IReadOnlyList<ServiceRevenueResponse> GetRevenuePerService() =>
        services.GetRevenuePerService().Select(item => new ServiceRevenueResponse
        {
            WashServiceId = item.WashService.Id,
            Name = item.WashService.Name,
            Revenue = item.Revenue
        }).ToList();
}
