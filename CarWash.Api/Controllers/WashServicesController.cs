using CarWash.Api.Requests;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// CRUD-операции с услугами автомойки
/// </summary>
[ApiController]
[Route("api/wash-services")]
public class WashServicesController(
    IWashServiceRepository washServiceRepository,
    IOrderRepository orderRepository) : ControllerBase
{
    /// <summary>
    /// Получить все услуги
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<WashService>> GetAll() =>
        Ok(washServiceRepository.GetAll());

    /// <summary>
    /// Получить услугу по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<WashService> GetById(int id)
    {
        var washService = washServiceRepository.GetById(id);
        return washService is null ? NotFound() : Ok(washService);
    }

    /// <summary>
    /// Создать услугу
    /// </summary>
    [HttpPost]
    public ActionResult<WashService> Create(WashServiceRequest request)
    {
        var washService = new WashService
        {
            Id = washServiceRepository.GetAll().Select(item => item.Id).DefaultIfEmpty().Max() + 1,
            Name = request.Name,
            Category = request.Category,
            Cost = request.Cost,
            DurationMinutes = request.DurationMinutes
        };

        washServiceRepository.Add(washService);
        return CreatedAtAction(nameof(GetById), new { id = washService.Id }, washService);
    }

    /// <summary>
    /// Изменить услугу
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, WashServiceRequest request)
    {
        var washService = washServiceRepository.GetById(id);
        if (washService is null)
            return NotFound();

        try
        {
            foreach (var order in orderRepository.GetAll().Where(order => order.WashService.Id == id))
                _ = order.StartTime.AddMinutes(request.DurationMinutes);
        }
        catch (ArgumentOutOfRangeException)
        {
            return BadRequest(new { message = "Дата окончания заказа выходит за допустимый диапазон." });
        }

        washService.Name = request.Name;
        washService.Category = request.Category;
        washService.Cost = request.Cost;
        washService.DurationMinutes = request.DurationMinutes;
        washServiceRepository.Update(washService);
        return NoContent();
    }

    /// <summary>
    /// Удалить услугу, если она не используется в заказах
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (washServiceRepository.GetById(id) is null)
            return NotFound();

        if (orderRepository.GetAll().Any(order => order.WashService.Id == id))
            return Conflict(new { message = "Услуга используется в заказах." });

        washServiceRepository.Delete(id);
        return NoContent();
    }
}
