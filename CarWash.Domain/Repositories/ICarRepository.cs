using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий автомобилей
/// </summary>
public interface ICarRepository
{
    /// <summary>
    /// Получить все автомобили
    /// </summary>
    IReadOnlyList<Car> GetAll();

    /// <summary>
    /// Получить автомобиль по идентификатору
    /// </summary>
    Car? GetById(int id);

    /// <summary>
    /// Добавить автомобиль
    /// </summary>
    void Add(Car car);

    /// <summary>
    /// Обновить автомобиль
    /// </summary>
    void Update(Car car);

    /// <summary>
    /// Удалить автомобиль
    /// </summary>
    void Delete(int id);
}