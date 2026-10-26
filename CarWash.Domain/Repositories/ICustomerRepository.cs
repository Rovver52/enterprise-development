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
    /// Получить пять клиентов с наибольшим количеством посещений
    /// </summary>
    IReadOnlyList<(Customer Customer, int VisitCount)> GetTop5CustomersByVisits();
}
