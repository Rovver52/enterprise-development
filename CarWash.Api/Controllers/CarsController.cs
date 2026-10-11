using CarWash.Api.Requests;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// CRUD-операции с автомобилями
/// </summary>
[ApiController]
[Route("api/cars")]
public class CarsController(
    ICarRepository carRepository,
    ICustomerRepository customerRepository,
    IOrderRepository orderRepository) : ControllerBase
{
    /// <summary>
    /// Получить все автомобили
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<Car>> GetAll() =>
        Ok(carRepository.GetAll());

    /// <summary>
    /// Получить автомобиль по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<Car> GetById(int id)
    {
        var car = carRepository.GetById(id);
        return car is null ? NotFound() : Ok(car);
    }

    /// <summary>
    /// Создать автомобиль
    /// </summary>
    [HttpPost]
    public ActionResult<Car> Create(CarRequest request)
    {
        var car = new Car
        {
            Id = carRepository.GetAll().Select(item => item.Id).DefaultIfEmpty().Max() + 1,
            LicensePlate = request.LicensePlate,
            Brand = request.Brand
        };

        carRepository.Add(car);
        return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
    }

    /// <summary>
    /// Изменить автомобиль
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, CarRequest request)
    {
        var car = carRepository.GetById(id);
        if (car is null)
            return NotFound();

        // Меняем существующий объект: на него ссылаются клиенты и заказы.
        car.LicensePlate = request.LicensePlate;
        car.Brand = request.Brand;
        carRepository.Update(car);
        return NoContent();
    }

    /// <summary>
    /// Удалить автомобиль, если он не используется в заказах
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (carRepository.GetById(id) is null)
            return NotFound();

        if (orderRepository.GetAll().Any(order => order.Car.Id == id))
            return Conflict(new { message = "Автомобиль используется в заказах." });

        foreach (var customer in customerRepository.GetAll())
            customer.Cars.RemoveAll(car => car.Id == id);

        carRepository.Delete(id);
        return NoContent();
    }
}
