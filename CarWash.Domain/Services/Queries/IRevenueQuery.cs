using CarWash.Domain.Models;

namespace CarWash.Domain.Services.Queries;

/// <summary>
/// Запрос на получение выручки по услугам.
/// </summary>
public interface IRevenueQuery
{
    /// <summary>
    /// Получить суммарную выручку по каждой услуге.
    /// </summary>
    List<(WashService Service, decimal TotalRevenue)> GetRevenuePerService();
}