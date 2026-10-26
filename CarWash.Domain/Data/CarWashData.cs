using CarWash.Domain.Models;

namespace CarWash.Domain.Data;

/// <summary>
/// Общие начальные данные сервера и тестов
/// </summary>
public class CarWashData
{
    /// <summary>
    /// Автомобили
    /// </summary>
    public IReadOnlyList<Car> Cars { get; }

    /// <summary>
    /// Клиенты
    /// </summary>
    public IReadOnlyList<Customer> Customers { get; }

    /// <summary>
    /// Услуги
    /// </summary>
    public IReadOnlyList<WashService> WashServices { get; }

    /// <summary>
    /// Заказы
    /// </summary>
    public IReadOnlyList<Order> Orders { get; }

    /// <summary>
    /// Создать фиксированный набор данных с одним текущим заказом
    /// </summary>
    public CarWashData()
    {
        Customers =
        [
            new() { Id = 1, FullName = "Иванов Иван Иванович", Phone = "79000000001" },
            new() { Id = 2, FullName = "Петров Петр Петрович", Phone = "79000000002" },
            new() { Id = 3, FullName = "Сидоров Алексей Олегович", Phone = "79000000003" },
            new() { Id = 4, FullName = "Смирнов Максим Игоревич", Phone = "79000000004" },
            new() { Id = 5, FullName = "Кузнецов Андрей Сергеевич", Phone = "79000000005" },
            new() { Id = 6, FullName = "Попов Дмитрий Андреевич", Phone = "79000000006" }
        ];

        Cars =
        [
            new() { Id = 1, LicensePlate = "A001AA", Brand = "Toyota", Customer = Customers[0] },
            new() { Id = 2, LicensePlate = "A002AA", Brand = "Kia", Customer = Customers[1] },
            new() { Id = 3, LicensePlate = "A003AA", Brand = "Lada", Customer = Customers[2] },
            new() { Id = 4, LicensePlate = "A004AA", Brand = "BMW", Customer = Customers[3] },
            new() { Id = 5, LicensePlate = "A005AA", Brand = "Audi", Customer = Customers[4] },
            new() { Id = 6, LicensePlate = "A006AA", Brand = "Ford", Customer = Customers[5] }
        ];

        foreach (var car in Cars)
            car.Customer.Cars.Add(car);

        WashServices =
        [
            new() { Id = 1, Name = "Стандартная мойка", Category = CarCategory.Sedan, Cost = 500m, DurationMinutes = 30 },
            new() { Id = 2, Name = "Мойка кузова", Category = CarCategory.Sedan, Cost = 400m, DurationMinutes = 20 },
            new() { Id = 3, Name = "Комплексная мойка", Category = CarCategory.Sedan, Cost = 800m, DurationMinutes = 40 },
            new() { Id = 4, Name = "Мойка внедорожника", Category = CarCategory.SUV, Cost = 1000m, DurationMinutes = 45 },
            new() { Id = 5, Name = "Мойка грузовика", Category = CarCategory.Truck, Cost = 1500m, DurationMinutes = 60 },
            new() { Id = 6, Name = "Мойка мотоцикла", Category = CarCategory.Motorcycle, Cost = 300m, DurationMinutes = 15 }
        ];

        var now = DateTime.Now;
        var orders = new List<Order>();
        for (var customerIndex = 0; customerIndex < 5; customerIndex++)
        {
            for (var visit = 0; visit < 5 - customerIndex; visit++)
            {
                orders.Add(new Order
                {
                    Id = orders.Count + 1,
                    Customer = Customers[customerIndex],
                    Car = Cars[customerIndex],
                    WashService = WashServices[customerIndex],
                    StartTime = now.AddDays(-1),
                    BoxNumber = customerIndex + 1
                });
            }
        }

        orders.Add(new Order
        {
            Id = 16,
            Customer = Customers[0],
            Car = Cars[0],
            WashService = WashServices[0],
            StartTime = now.AddMinutes(-5),
            BoxNumber = 1
        });
        Orders = orders;
    }
}
