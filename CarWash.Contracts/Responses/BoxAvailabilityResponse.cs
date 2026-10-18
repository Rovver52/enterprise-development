namespace CarWash.Contracts.Responses;

/// <summary>
/// Время освобождения бокса
/// </summary>
public class BoxAvailabilityResponse
{
    /// <summary>
    /// Номер бокса
    /// </summary>
    public required int BoxNumber { get; set; }

    /// <summary>
    /// Время, с которого бокс свободен
    /// </summary>
    public required DateTime AvailableFrom { get; set; }
}
