namespace CarWash.Domain.Models;

/// <summary>
/// Услуга автомойки.
/// Характеризуется названием, категорией автомобиля, стоимостью и длительностью в минутах.
/// </summary>
public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CarCategory Category { get; set; }
    public decimal Cost { get; set; }
    public int DurationMinutes { get; set; }

    public Service() { }

    public Service(int id, string name, CarCategory category, decimal cost, int durationMinutes)
    {
        Id = id;
        Name = name;
        Category = category;
        Cost = cost;
        DurationMinutes = durationMinutes;
    }

    public override string ToString() => $"{Name} ({Category}) — {Cost} руб., {DurationMinutes} мин.";
}