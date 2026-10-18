using CarWash.Application.Services;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Операции с услугами
/// </summary>
[ApiController]
[Route("api/wash-services")]
public class WashServicesController(WashServiceService service) : ControllerBase
{
    /// <summary>
    /// Получить список
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<WashServiceResponse>> GetAll() => Ok(service.GetAll());

    /// <summary>
    /// Получить услугу по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<WashServiceResponse> GetById(int id) => Ok(service.GetById(id));

    /// <summary>
    /// Создать услугу
    /// </summary>
    [HttpPost]
    [ProducesResponseType<WashServiceResponse>(StatusCodes.Status201Created)]
    public ActionResult<WashServiceResponse> Create(WashServiceRequest request)
    {
        var response = service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Изменить услугу
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Update(int id, WashServiceRequest request)
    {
        service.Update(id, request);
        return NoContent();
    }

    /// <summary>
    /// Удалить услугу
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        service.Delete(id);
        return NoContent();
    }

    /// <summary>
    /// Получить заказы с услугой
    /// </summary>
    [HttpGet("{id:int}/orders")]
    public ActionResult<IReadOnlyList<OrderResponse>> GetOrders(int id) => Ok(service.GetOrders(id));
}
