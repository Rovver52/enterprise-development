using System.ComponentModel.DataAnnotations;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Операции с коллекцией клиентов
/// </summary>
[ApiController]
[Route("api/customers")]
[ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
public class CustomersController(ICustomerService service, ILogger<CustomersController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список клиентов
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<CustomerResponse>>(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<CustomerResponse>> GetAll()
    {
        logger.LogInformation("Получение списка клиентов");
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<CustomerResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public ActionResult<CustomerResponse> GetById([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        logger.LogInformation("Получение записи из коллекции клиентов: {Id}", id);
        return Ok(service.GetById(id));
    }

    /// <summary>
    /// Создать клиента
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CustomerResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status415UnsupportedMediaType)]
    public ActionResult<CustomerResponse> Create(CustomerRequest request)
    {
        var response = service.Create(request);
        logger.LogInformation("Создана запись в коллекции клиентов: {Id}", response.Id);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Изменить клиента
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status415UnsupportedMediaType)]
    public IActionResult Update([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CustomerRequest request)
    {
        service.Update(id, request);
        logger.LogInformation("Изменена запись в коллекции клиентов: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status409Conflict)]
    public IActionResult Delete([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        service.Delete(id);
        logger.LogInformation("Удалена запись из коллекции клиентов: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Получить автомобили клиента
    /// </summary>
    [HttpGet("{id:int}/cars")]
    [ProducesResponseType<IReadOnlyList<CarResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public ActionResult<IReadOnlyList<CarResponse>> GetCars([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        logger.LogInformation("Запрос: автомобили клиента; Id: {Id}", id);
        return Ok(service.GetCars(id));
    }

    /// <summary>
    /// Получить заказы клиента
    /// </summary>
    [HttpGet("{id:int}/orders")]
    [ProducesResponseType<IReadOnlyList<OrderResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public ActionResult<IReadOnlyList<OrderResponse>> GetOrders([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        logger.LogInformation("Запрос: заказы клиента; Id: {Id}", id);
        return Ok(service.GetOrders(id));
    }
}
