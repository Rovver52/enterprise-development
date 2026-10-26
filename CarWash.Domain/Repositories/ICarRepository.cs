using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий автомобилей
/// </summary>
public interface ICarRepository : IRepository<Car>
{
    /// <summary>
    /// Получить автомобили владельца
    /// </summary>
    IReadOnlyList<Car> GetByCustomerId(int customerId);

    /// <summary>
    /// Получить владельца автомобиля
    /// </summary>
    Customer? GetCustomer(int carId);
}
