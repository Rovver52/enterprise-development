using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий автомобилей в памяти
/// </summary>
public class InMemoryCarRepository(List<Car> cars) : ICarRepository
{
    /// <summary>
    /// Получить все автомобили
    /// </summary>
    public IReadOnlyList<Car> GetAll() =>
        cars.AsReadOnly();

    /// <summary>
    /// Получить автомобиль по идентификатору
    /// </summary>
    public Car? GetById(int id) =>
        cars.FirstOrDefault(car => car.Id == id);

    /// <summary>
    /// Добавить автомобиль
    /// </summary>
    public void Add(Car car) =>
        cars.Add(car);

    /// <summary>
    /// Обновить автомобиль
    /// </summary>
    public void Update(Car car)
    {
        var index = cars.FindIndex(item => item.Id == car.Id);

        if (index >= 0)
            cars[index] = car;
    }

    /// <summary>
    /// Удалить автомобиль
    /// </summary>
    public void Delete(int id) =>
        cars.RemoveAll(car => car.Id == id);
}