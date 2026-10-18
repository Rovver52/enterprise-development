using CarWash.Application.Services;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Операции с заказами
/// </summary>
[ApiController]
[Route("api/orders")]
public class OrdersController(OrderService service) : ControllerBase
{
    /// <summary>
    /// Получить список
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<OrderResponse>> GetAll() => Ok(service.GetAll());

    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<OrderResponse> GetById(int id) => Ok(service.GetById(id));

    /// <summary>
    /// Создать заказ
    /// </summary>
    [HttpPost]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status201Created)]
    public ActionResult<OrderResponse> Create(OrderRequest request)
    {
        var response = service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Изменить заказ
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Update(int id, OrderRequest request)
    {
        service.Update(id, request);
        return NoContent();
    }

    /// <summary>
    /// Удалить заказ
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        service.Delete(id);
        return NoContent();
    }

    /// <summary>
    /// Получить клиента заказа
    /// </summary>
    [HttpGet("{id:int}/customer")]
    public ActionResult<CustomerResponse> GetCustomer(int id) => Ok(service.GetCustomer(id));

    /// <summary>
    /// Получить автомобиль заказа
    /// </summary>
    [HttpGet("{id:int}/car")]
    public ActionResult<CarResponse> GetCar(int id) => Ok(service.GetCar(id));

    /// <summary>
    /// Получить услугу заказа
    /// </summary>
    [HttpGet("{id:int}/wash-service")]
    public ActionResult<WashServiceResponse> GetWashService(int id) => Ok(service.GetWashService(id));
}
