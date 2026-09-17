namespace CarWash.Domain.Services.Queries;

/// <summary>
/// Запрос на получение времени освобождения бокса.
/// </summary>
public interface IBoxAvailabilityQuery
{
    /// <summary>
    /// Получить время, когда бокс освободится.
    /// </summary>
    DateTime? GetBoxFreeTime(int boxNumber, DateTime? now = null);
}