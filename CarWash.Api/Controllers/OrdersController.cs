using System.ComponentModel.DataAnnotations;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Операции с коллекцией заказов
/// </summary>
[ApiController]
[Route("api/orders")]
[ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
public class OrdersController(IOrderService service, ILogger<OrdersController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список заказов
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<OrderResponse>>(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<OrderResponse>> GetAll()
    {
        logger.LogInformation("Получение списка заказов");
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public ActionResult<OrderResponse> GetById([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        logger.LogInformation("Получение записи из коллекции заказов: {Id}", id);
        return Ok(service.GetById(id));
    }

    /// <summary>
    /// Создать заказ
    /// </summary>
    [HttpPost]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status415UnsupportedMediaType)]
    public ActionResult<OrderResponse> Create(OrderRequest request)
    {
        var response = service.Create(request);
        logger.LogInformation("Создана запись в коллекции заказов: {Id}", response.Id);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Изменить заказ
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status415UnsupportedMediaType)]
    public IActionResult Update([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, OrderRequest request)
    {
        service.Update(id, request);
        logger.LogInformation("Изменена запись в коллекции заказов: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Удалить заказ
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public IActionResult Delete([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        service.Delete(id);
        logger.LogInformation("Удалена запись из коллекции заказов: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Получить клиента заказа
    /// </summary>
    [HttpGet("{id:int}/customer")]
    [ProducesResponseType<CustomerResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public ActionResult<CustomerResponse> GetCustomer([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        logger.LogInformation("Запрос: клиента заказа; Id: {Id}", id);
        return Ok(service.GetCustomer(id));
    }

    /// <summary>
    /// Получить автомобиль заказа
    /// </summary>
    [HttpGet("{id:int}/car")]
    [ProducesResponseType<CarResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public ActionResult<CarResponse> GetCar([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        logger.LogInformation("Запрос: автомобиль заказа; Id: {Id}", id);
        return Ok(service.GetCar(id));
    }

    /// <summary>
    /// Получить услугу заказа
    /// </summary>
    [HttpGet("{id:int}/wash-service")]
    [ProducesResponseType<WashServiceResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public ActionResult<WashServiceResponse> GetWashService([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id)
    {
        logger.LogInformation("Запрос: услугу заказа; Id: {Id}", id);
        return Ok(service.GetWashService(id));
    }
}
