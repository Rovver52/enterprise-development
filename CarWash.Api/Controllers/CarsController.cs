using CarWash.Application.Services;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Операции с автомобилями
/// </summary>
[ApiController]
[Route("api/cars")]
public class CarsController(CarService service) : ControllerBase
{
    /// <summary>
    /// Получить список
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<CarResponse>> GetAll() => Ok(service.GetAll());

    /// <summary>
    /// Получить автомобиль по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<CarResponse> GetById(int id) => Ok(service.GetById(id));

    /// <summary>
    /// Создать автомобиль
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CarResponse>(StatusCodes.Status201Created)]
    public ActionResult<CarResponse> Create(CarRequest request)
    {
        var response = service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Изменить автомобиль
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Update(int id, CarRequest request)
    {
        service.Update(id, request);
        return NoContent();
    }

    /// <summary>
    /// Удалить автомобиль
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        service.Delete(id);
        return NoContent();
    }

    /// <summary>
    /// Получить клиентов автомобиля
    /// </summary>
    [HttpGet("{id:int}/customers")]
    public ActionResult<IReadOnlyList<CustomerResponse>> GetCustomers(int id) => Ok(service.GetCustomers(id));

    /// <summary>
    /// Получить заказы автомобиля
    /// </summary>
    [HttpGet("{id:int}/orders")]
    public ActionResult<IReadOnlyList<OrderResponse>> GetOrders(int id) => Ok(service.GetOrders(id));
}
