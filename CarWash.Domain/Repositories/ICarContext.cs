namespace CarWash.Domain.Repositories;

using CarWash.Domain.Models;

/// <summary>
/// Контекст для работы с автомобилями
/// </summary>
public interface ICarContext
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
    /// Обновить данные автомобиля
    /// </summary>
    void Update(Car car);

    /// <summary>
    /// Удалить автомобиль по идентификатору
    /// </summary>
    void Delete(int id);
}