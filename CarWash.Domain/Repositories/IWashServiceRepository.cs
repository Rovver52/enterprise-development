using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий услуг автомойки
/// </summary>
public interface IWashServiceRepository : IRepository<WashService>
{
    /// <summary>
    /// Получить пять наиболее популярных услуг
    /// </summary>
    IReadOnlyList<(WashService WashService, int OrderCount)> GetTop5PopularServices();

    /// <summary>
    /// Получить общую выручку по каждой услуге
    /// </summary>
    IReadOnlyList<(WashService WashService, decimal Revenue)> GetRevenuePerService();
}
