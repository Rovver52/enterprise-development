using System.ComponentModel.DataAnnotations;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Операции с коллекцией услуг
/// </summary>
[ApiController]
[Route("api/wash-services")]
[ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
public class WashServicesController(IWashServiceService service, ILogger<WashServicesController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список услуг
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<WashServiceResponse>>(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<WashServiceResponse>> GetAll()
    {
        logger.LogInformation("Получение списка услуг");
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получить услугу по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<WashServiceResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public ActionResult<WashServiceResponse> GetById([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        logger.LogInformation("Получение записи из коллекции услуг: {Id}", id);
        return Ok(service.GetById(id));
    }

    /// <summary>
    /// Создать услугу
    /// </summary>
    [HttpPost]
    [ProducesResponseType<WashServiceResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status415UnsupportedMediaType)]
    public ActionResult<WashServiceResponse> Create(WashServiceRequest request)
    {
        var response = service.Create(request);
        logger.LogInformation("Создана запись в коллекции услуг: {Id}", response.Id);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Изменить услугу
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status415UnsupportedMediaType)]
    public IActionResult Update([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, WashServiceRequest request)
    {
        service.Update(id, request);
        logger.LogInformation("Изменена запись в коллекции услуг: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Удалить услугу
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status409Conflict)]
    public IActionResult Delete([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        service.Delete(id);
        logger.LogInformation("Удалена запись из коллекции услуг: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Получить заказы с услугой
    /// </summary>
    [HttpGet("{id:int}/orders")]
    [ProducesResponseType<IReadOnlyList<OrderResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public ActionResult<IReadOnlyList<OrderResponse>> GetOrders([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        logger.LogInformation("Запрос: заказы с услугой; Id: {Id}", id);
        return Ok(service.GetOrders(id));
    }
}
