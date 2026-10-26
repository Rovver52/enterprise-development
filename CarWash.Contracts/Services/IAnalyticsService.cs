using CarWash.Contracts.Responses;

namespace CarWash.Contracts.Services;

/// <summary>
/// Аналитические запросы автомойки
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить пять клиентов с наибольшим количеством посещений
    /// </summary>
    IReadOnlyList<TopCustomerResponse> GetTopCustomers();

    /// <summary>
    /// Получить автомобили, находящиеся сейчас на мойке
    /// </summary>
    IReadOnlyList<CarResponse> GetCurrentCars();

    /// <summary>
    /// Получить пять наиболее популярных услуг
    /// </summary>
    IReadOnlyList<PopularServiceResponse> GetTopServices();

    /// <summary>
    /// Получить время освобождения бокса
    /// </summary>
    BoxAvailabilityResponse GetBoxAvailability(int boxNumber);

    /// <summary>
    /// Получить выручку по каждой услуге
    /// </summary>
    IReadOnlyList<ServiceRevenueResponse> GetRevenuePerService();
}
