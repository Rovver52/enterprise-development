using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Контекст автомобилей в памяти.
/// </summary>
public class InMemoryCarContext : ICarContext
{
    private readonly List<Car> _cars = new();

    public IReadOnlyList<Car> GetAll() => _cars.AsReadOnly();
    public Car? GetById(int id) => _cars.FirstOrDefault(c => c.Id == id);
    public void Add(Car car) => _cars.Add(car);
    public void Update(Car car)
    {
        var index = _cars.FindIndex(c => c.Id == car.Id);
        if (index >= 0) _cars[index] = car;
    }
    public void Delete(int id)
    {
        var car = _cars.FirstOrDefault(c => c.Id == id);
        if (car != null) _cars.Remove(car);
    }
}