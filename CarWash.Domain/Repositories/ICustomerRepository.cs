using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий клиентов
/// </summary>
public interface ICustomerRepository : IRepository<Customer>
{
    /// <summary>
    /// Получить автомобили клиента
    /// </summary>
    IReadOnlyList<Car> GetCars(int customerId);

    /// <summary>
    /// Получить клиентов, связанных с автомобилем
    /// </summary>
    IReadOnlyList<Customer> GetByCarId(int carId);

    /// <summary>
    /// Связать клиента с существующим автомобилем
    /// </summary>
    void AddCar(int customerId, Car car);

    /// <summary>
    /// Удалить связь клиента с автомобилем
    /// </summary>
    void RemoveCar(int customerId, int carId);

    /// <summary>
    /// Удалить связи всех клиентов с автомобилем
    /// </summary>
    void RemoveCarFromAll(int carId);

    /// <summary>
    /// Получить пять клиентов с наибольшим количеством посещений
    /// </summary>
    IReadOnlyList<(Customer Customer, int VisitCount)> GetTop5CustomersByVisits();
}
