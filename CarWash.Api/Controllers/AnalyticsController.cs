using CarWash.Application.Services;
using CarWash.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Аналитические запросы автомойки
/// </summary>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(AnalyticsService service) : ControllerBase
{
    /// <summary>
    /// Получить пять клиентов с наибольшим количеством посещений
    /// </summary>
    [HttpGet("top-customers")]
    public ActionResult<IReadOnlyList<TopCustomerResponse>> GetTopCustomers() => Ok(service.GetTopCustomers());

    /// <summary>
    /// Получить автомобили, которые сейчас находятся на мойке
    /// </summary>
    [HttpGet("current-cars")]
    public ActionResult<IReadOnlyList<CarResponse>> GetCurrentCars() => Ok(service.GetCurrentCars());

    /// <summary>
    /// Получить пять наиболее популярных услуг
    /// </summary>
    [HttpGet("top-services")]
    public ActionResult<IReadOnlyList<PopularServiceResponse>> GetTopServices() => Ok(service.GetTopServices());

    /// <summary>
    /// Получить время освобождения бокса
    /// </summary>
    [HttpGet("boxes/{boxNumber:int}/available-from")]
    public ActionResult<BoxAvailabilityResponse> GetBoxAvailability(int boxNumber) =>
        Ok(service.GetBoxAvailability(boxNumber));

    /// <summary>
    /// Получить выручку по каждой услуге
    /// </summary>
    [HttpGet("revenue-per-service")]
    public ActionResult<IReadOnlyList<ServiceRevenueResponse>> GetRevenuePerService() =>
        Ok(service.GetRevenuePerService());
}
