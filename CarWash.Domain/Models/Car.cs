namespace CarWash.Domain.Models;

/// <summary>
/// Автомобиль клиента.
/// Характеризуется госномером и маркой.
/// </summary>
public class Car
{
    public int Id { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public Client? Client { get; set; }

    public Car() { }

    public Car(int id, string licensePlate, string brand, int clientId)
    {
        Id = id;
        LicensePlate = licensePlate;
        Brand = brand;
        ClientId = clientId;
    }

    public override string ToString() => $"{Brand} [{LicensePlate}]";
}