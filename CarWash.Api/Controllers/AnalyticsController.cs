using System.ComponentModel.DataAnnotations;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Аналитические запросы автомойки
/// </summary>
[ApiController]
[Route("api/analytics")]
[ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить пять клиентов с наибольшим количеством посещений
    /// </summary>
    [HttpGet("top-customers")]
    [ProducesResponseType<IReadOnlyList<TopCustomerResponse>>(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<TopCustomerResponse>> GetTopCustomers()
    {
        logger.LogInformation("Запрос аналитики: пять клиентов с наибольшим количеством посещений");
        return Ok(service.GetTopCustomers());
    }

    /// <summary>
    /// Получить автомобили, находящиеся сейчас на мойке
    /// </summary>
    [HttpGet("current-cars")]
    [ProducesResponseType<IReadOnlyList<CarResponse>>(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<CarResponse>> GetCurrentCars()
    {
        logger.LogInformation("Запрос аналитики: автомобили, находящиеся сейчас на мойке");
        return Ok(service.GetCurrentCars());
    }

    /// <summary>
    /// Получить пять наиболее популярных услуг
    /// </summary>
    [HttpGet("top-services")]
    [ProducesResponseType<IReadOnlyList<PopularServiceResponse>>(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<PopularServiceResponse>> GetTopServices()
    {
        logger.LogInformation("Запрос аналитики: пять наиболее популярных услуг");
        return Ok(service.GetTopServices());
    }

    /// <summary>
    /// Получить время освобождения бокса
    /// </summary>
    [HttpGet("boxes/{boxNumber:int}/available-from")]
    [ProducesResponseType<BoxAvailabilityResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    public ActionResult<BoxAvailabilityResponse> GetBoxAvailability([Range(1, int.MaxValue, ErrorMessage = "Номер бокса должен быть положительным.")] int boxNumber)
    {
        logger.LogInformation("Запрос времени освобождения бокса {BoxNumber}", boxNumber);
        return Ok(service.GetBoxAvailability(boxNumber));
    }

    /// <summary>
    /// Получить выручку по каждой услуге
    /// </summary>
    [HttpGet("revenue-per-service")]
    [ProducesResponseType<IReadOnlyList<ServiceRevenueResponse>>(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<ServiceRevenueResponse>> GetRevenuePerService()
    {
        logger.LogInformation("Запрос аналитики: выручку по каждой услуге");
        return Ok(service.GetRevenuePerService());
    }
}
