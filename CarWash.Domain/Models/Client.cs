namespace CarWash.Domain.Models;

/// <summary>
/// Клиент автомойки.
/// Характеризуется ФИО и телефоном. Может иметь несколько автомобилей.
/// </summary>
public class Client
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public List<Car> Cars { get; set; } = new();

    public Client() { }

    public Client(int id, string fullName, string phone)
    {
        Id = id;
        FullName = fullName;
        Phone = phone;
    }

    public void AddCar(Car car)
    {
        Cars.Add(car);
    }

    public override string ToString() => $"{FullName} ({Phone})";
}