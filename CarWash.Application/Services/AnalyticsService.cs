using CarWash.Application.Exceptions;
using CarWash.Application.Mapping;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Подготовка ответов аналитических запросов
/// </summary>
public class AnalyticsService(
    ICustomerRepository customers,
    IOrderRepository orders,
    IWashServiceRepository services) : IAnalyticsService
{
    /// <inheritdoc />
    public IReadOnlyList<TopCustomerResponse> GetTopCustomers() =>
        customers.GetTop5CustomersByVisits().Select(item => new TopCustomerResponse
        {
            CustomerId = item.Customer.Id,
            FullName = item.Customer.FullName,
            VisitCount = item.VisitCount
        }).ToList();

    /// <inheritdoc />
    public IReadOnlyList<CarResponse> GetCurrentCars() =>
        orders.GetCarsCurrentlyAtWash().Select(car => car.ToResponse()).ToList();

    /// <inheritdoc />
    public IReadOnlyList<PopularServiceResponse> GetTopServices() =>
        services.GetTop5PopularServices().Select(item => new PopularServiceResponse
        {
            WashServiceId = item.WashService.Id,
            Name = item.WashService.Name,
            OrderCount = item.OrderCount
        }).ToList();

    /// <inheritdoc />
    public BoxAvailabilityResponse GetBoxAvailability(int boxNumber)
    {
        if (boxNumber <= 0)
            throw new RequestValidationException(nameof(boxNumber), "Номер бокса должен быть положительным.");
        return new BoxAvailabilityResponse
        {
            BoxNumber = boxNumber,
            AvailableFrom = orders.GetBoxAvailableFrom(boxNumber)
        };
    }

    /// <inheritdoc />
    public IReadOnlyList<ServiceRevenueResponse> GetRevenuePerService() =>
        services.GetRevenuePerService().Select(item => new ServiceRevenueResponse
        {
            WashServiceId = item.WashService.Id,
            Name = item.WashService.Name,
            Revenue = item.Revenue
        }).ToList();
}
