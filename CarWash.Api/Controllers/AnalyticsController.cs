using CarWash.Domain.Models;
using CarWash.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Пять аналитических запросов из первой лабораторной работы
/// </summary>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(
    CustomerAnalyticsService customerAnalyticsService,
    OrderAnalyticsService orderAnalyticsService,
    WashServiceAnalyticsService washServiceAnalyticsService) : ControllerBase
{
    /// <summary>
    /// Получить пять клиентов с наибольшим количеством посещений
    /// </summary>
    [HttpGet("top-customers")]
    public IActionResult GetTopCustomers() =>
        Ok(customerAnalyticsService.GetTop5CustomersByVisits()
            .Select(item => new { item.Customer, item.VisitCount }));

    /// <summary>
    /// Получить автомобили, которые сейчас находятся на мойке
    /// </summary>
    [HttpGet("current-cars")]
    public ActionResult<IReadOnlyList<Car>> GetCurrentCars() =>
        Ok(orderAnalyticsService.GetCarsCurrentlyAtWash());

    /// <summary>
    /// Получить пять наиболее популярных услуг
    /// </summary>
    [HttpGet("top-services")]
    public IActionResult GetTopServices() =>
        Ok(washServiceAnalyticsService.GetTop5PopularServices()
            .Select(item => new { item.WashService, item.OrderCount }));

    /// <summary>
    /// Получить время освобождения выбранного бокса
    /// </summary>
    [HttpGet("boxes/{boxNumber:int}/available-from")]
    public IActionResult GetBoxAvailableFrom(int boxNumber)
    {
        if (boxNumber < 1)
            return BadRequest(new { message = "Номер бокса должен быть положительным." });

        return Ok(new
        {
            BoxNumber = boxNumber,
            AvailableFrom = orderAnalyticsService.GetBoxAvailableFrom(boxNumber)
        });
    }

    /// <summary>
    /// Получить общую выручку по каждой услуге
    /// </summary>
    [HttpGet("revenue-per-service")]
    public IActionResult GetRevenuePerService() =>
        Ok(washServiceAnalyticsService.GetRevenuePerService()
            .Select(item => new { item.WashService, item.Revenue }));
}
