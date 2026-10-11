using CarWash.Api.Requests;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// CRUD-операции с заказами
/// </summary>
[ApiController]
[Route("api/orders")]
public class OrdersController(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    ICarRepository carRepository,
    IWashServiceRepository washServiceRepository) : ControllerBase
{
    /// <summary>
    /// Получить все заказы
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<Order>> GetAll() =>
        Ok(orderRepository.GetAll());

    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<Order> GetById(int id)
    {
        var order = orderRepository.GetById(id);
        return order is null ? NotFound() : Ok(order);
    }

    /// <summary>
    /// Создать заказ для существующих клиента, автомобиля и услуги
    /// </summary>
    [HttpPost]
    public ActionResult<Order> Create(OrderRequest request)
    {
        var id = orderRepository.GetAll().Select(item => item.Id).DefaultIfEmpty().Max() + 1;
        var order = CreateOrder(id, request);
        if (order is null)
            return ValidationProblem(ModelState);

        orderRepository.Add(order);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    /// <summary>
    /// Изменить заказ
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, OrderRequest request)
    {
        if (orderRepository.GetById(id) is null)
            return NotFound();

        var order = CreateOrder(id, request);
        if (order is null)
            return ValidationProblem(ModelState);

        orderRepository.Update(order);
        return NoContent();
    }

    /// <summary>
    /// Удалить заказ
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (orderRepository.GetById(id) is null)
            return NotFound();

        orderRepository.Delete(id);
        return NoContent();
    }

    private Order? CreateOrder(int id, OrderRequest request)
    {
        var customer = customerRepository.GetById(request.CustomerId);
        var car = carRepository.GetById(request.CarId);
        var washService = washServiceRepository.GetById(request.WashServiceId);

        if (customer is null)
            ModelState.AddModelError(nameof(request.CustomerId), "Клиент не найден.");
        if (car is null)
            ModelState.AddModelError(nameof(request.CarId), "Автомобиль не найден.");
        if (washService is null)
            ModelState.AddModelError(nameof(request.WashServiceId), "Услуга не найдена.");

        if (customer is null || car is null || washService is null)
            return null;

        // IsInProgress в первой лабе сравнивает время с локальным DateTime.Now.
        var startTime = request.StartTime.Kind == DateTimeKind.Utc
            ? request.StartTime.ToLocalTime()
            : request.StartTime;

        try
        {
            _ = startTime.AddMinutes(washService.DurationMinutes);
        }
        catch (ArgumentOutOfRangeException)
        {
            ModelState.AddModelError(nameof(request.StartTime), "Дата окончания выходит за допустимый диапазон.");
            return null;
        }

        return new Order
        {
            Id = id,
            Customer = customer,
            Car = car,
            WashService = washService,
            StartTime = startTime,
            BoxNumber = request.BoxNumber
        };
    }
}
