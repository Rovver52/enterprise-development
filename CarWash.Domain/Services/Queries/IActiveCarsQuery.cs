using CarWash.Domain.Models;

namespace CarWash.Domain.Services.Queries;

/// <summary>
/// Запрос на получение автомобилей на мойке.
/// </summary>
public interface IActiveCarsQuery
{
    /// <summary>
    /// Получить автомобили, находящиеся на мойке в данный момент.
    /// </summary>
    List<Car> GetCarsCurrentlyAtWash();
}