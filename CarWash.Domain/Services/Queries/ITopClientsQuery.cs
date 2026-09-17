using CarWash.Domain.Models;

namespace CarWash.Domain.Services.Queries;

/// <summary>
/// Запрос на получение топ-5 клиентов по количеству посещений.
/// </summary>
public interface ITopClientsQuery
{
    /// <summary>
    /// Получить топ-5 клиентов по количеству посещений.
    /// </summary>
    List<(Customer Client, int VisitCount)> GetTop5ClientsByVisits();
}