using CarWash.Application.Services;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Операции с клиентами
/// </summary>
[ApiController]
[Route("api/customers")]
public class CustomersController(CustomerService service) : ControllerBase
{
    /// <summary>
    /// Получить список
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<CustomerResponse>> GetAll() => Ok(service.GetAll());

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<CustomerResponse> GetById(int id) => Ok(service.GetById(id));

    /// <summary>
    /// Создать клиента
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CustomerResponse>(StatusCodes.Status201Created)]
    public ActionResult<CustomerResponse> Create(CustomerRequest request)
    {
        var response = service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Изменить клиента
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Update(int id, CustomerRequest request)
    {
        service.Update(id, request);
        return NoContent();
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        service.Delete(id);
        return NoContent();
    }

    /// <summary>
    /// Получить автомобили клиента
    /// </summary>
    [HttpGet("{id:int}/cars")]
    public ActionResult<IReadOnlyList<CarResponse>> GetCars(int id) => Ok(service.GetCars(id));

    /// <summary>
    /// Получить заказы клиента
    /// </summary>
    [HttpGet("{id:int}/orders")]
    public ActionResult<IReadOnlyList<OrderResponse>> GetOrders(int id) => Ok(service.GetOrders(id));

    /// <summary>
    /// Связать клиента с существующим автомобилем
    /// </summary>
    [HttpPut("{id:int}/cars/{carId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AddCar(int id, int carId)
    {
        service.AddCar(id, carId);
        return NoContent();
    }

    /// <summary>
    /// Удалить связь клиента с автомобилем
    /// </summary>
    [HttpDelete("{id:int}/cars/{carId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult RemoveCar(int id, int carId)
    {
        service.RemoveCar(id, carId);
        return NoContent();
    }
}
