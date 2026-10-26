using System.ComponentModel.DataAnnotations;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Операции с коллекцией автомобилей
/// </summary>
[ApiController]
[Route("api/cars")]
[ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
public class CarsController(ICarService service, ILogger<CarsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список автомобилей
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<CarResponse>>(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<CarResponse>> GetAll()
    {
        logger.LogInformation("Получение списка автомобилей");
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получить автомобиль по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<CarResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public ActionResult<CarResponse> GetById([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        logger.LogInformation("Получение записи из коллекции автомобилей: {Id}", id);
        return Ok(service.GetById(id));
    }

    /// <summary>
    /// Создать автомобиль
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CarResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status415UnsupportedMediaType)]
    public ActionResult<CarResponse> Create(CarRequest request)
    {
        var response = service.Create(request);
        logger.LogInformation("Создана запись в коллекции автомобилей: {Id}", response.Id);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Изменить автомобиль
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status415UnsupportedMediaType)]
    public IActionResult Update([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CarRequest request)
    {
        service.Update(id, request);
        logger.LogInformation("Изменена запись в коллекции автомобилей: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Удалить автомобиль
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status409Conflict)]
    public IActionResult Delete([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        service.Delete(id);
        logger.LogInformation("Удалена запись из коллекции автомобилей: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Получить владельца автомобиля
    /// </summary>
    [HttpGet("{id:int}/customer")]
    [ProducesResponseType<CustomerResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public ActionResult<CustomerResponse> GetCustomer([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        logger.LogInformation("Запрос: владельца автомобиля; Id: {Id}", id);
        return Ok(service.GetCustomer(id));
    }

    /// <summary>
    /// Получить заказы автомобиля
    /// </summary>
    [HttpGet("{id:int}/orders")]
    [ProducesResponseType<IReadOnlyList<OrderResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public ActionResult<IReadOnlyList<OrderResponse>> GetOrders([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        logger.LogInformation("Запрос: заказы автомобиля; Id: {Id}", id);
        return Ok(service.GetOrders(id));
    }
}
