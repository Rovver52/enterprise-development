using CarWash.Api.Requests;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// CRUD-операции с клиентами
/// </summary>
[ApiController]
[Route("api/customers")]
public class CustomersController(
    ICustomerRepository customerRepository,
    ICarRepository carRepository,
    IOrderRepository orderRepository) : ControllerBase
{
    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<Customer>> GetAll() =>
        Ok(customerRepository.GetAll());

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<Customer> GetById(int id)
    {
        var customer = customerRepository.GetById(id);
        return customer is null ? NotFound() : Ok(customer);
    }

    /// <summary>
    /// Создать клиента
    /// </summary>
    [HttpPost]
    public ActionResult<Customer> Create(CustomerRequest request)
    {
        var cars = GetCars(request.CarIds);
        if (cars is null)
            return BadRequest(new { message = "Один из указанных автомобилей не найден." });

        var customer = new Customer
        {
            Id = customerRepository.GetAll().Select(item => item.Id).DefaultIfEmpty().Max() + 1,
            FullName = request.FullName,
            Phone = request.Phone,
            Cars = cars
        };

        customerRepository.Add(customer);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Изменить клиента и список его автомобилей
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, CustomerRequest request)
    {
        var customer = customerRepository.GetById(id);
        if (customer is null)
            return NotFound();

        var cars = GetCars(request.CarIds);
        if (cars is null)
            return BadRequest(new { message = "Один из указанных автомобилей не найден." });

        customer.FullName = request.FullName;
        customer.Phone = request.Phone;
        customer.Cars = cars;
        customerRepository.Update(customer);
        return NoContent();
    }

    /// <summary>
    /// Удалить клиента, если он не используется в заказах
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (customerRepository.GetById(id) is null)
            return NotFound();

        if (orderRepository.GetAll().Any(order => order.Customer.Id == id))
            return Conflict(new { message = "Клиент используется в заказах." });

        customerRepository.Delete(id);
        return NoContent();
    }

    private List<Car>? GetCars(IEnumerable<int> ids)
    {
        var cars = new List<Car>();
        foreach (var id in ids.Distinct())
        {
            var car = carRepository.GetById(id);
            if (car is null)
                return null;

            cars.Add(car);
        }

        return cars;
    }
}
