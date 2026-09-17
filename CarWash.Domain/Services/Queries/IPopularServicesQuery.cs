using CarWash.Domain.Models;

namespace CarWash.Domain.Services.Queries;

/// <summary>
/// Запрос на получение топ-5 популярных услуг.
/// </summary>
public interface IPopularServicesQuery
{
    /// <summary>
    /// Получить топ-5 наиболее популярных услуг.
    /// </summary>
    List<(WashService Service, int OrderCount)> GetTop5PopularServices();
}