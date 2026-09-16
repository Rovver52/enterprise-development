using Bogus;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using CarWash.Domain.Services;
using Xunit;

namespace CarWash.Tests;

public class CarWashServiceTests
{
    private readonly CarWashService _service;
    private readonly InMemoryOrderRepository _repository;
    private readonly List<Client> _clients;
    private readonly List<Service> _services;
    private readonly List<Car> _cars;
    private readonly List<Order> _orders;

    public CarWashServiceTests()
    {
        var faker = new Faker("ru");

        // Генерация услуг
        _services = new List<Service>
        {
            new(1, "Экспресс-мойка", CarCategory.Sedan, 500, 15),
            new(2, "Комплексная мойка", CarCategory.Sedan, 1200, 40),
            new(3, "Мойка двигателя", CarCategory.Sedan, 800, 30),
            new(4, "Химчистка салона", CarCategory.Sedan, 3000, 120),
            new(5, "Мойка грузовика", CarCategory.Truck, 2000, 60)
        };

        // Генерация клиентов
        var clientFaker = new Faker<Client>("ru")
            .RuleFor(c => c.Id, f => f.IndexFaker + 1)
            .RuleFor(c => c.FullName, f => f.Person.FullName)
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("+7 (###) ###-##-##"));

        _clients = clientFaker.Generate(10);

        // Генерация автомобилей
        var carBrands = new[] { "Toyota", "BMW", "Mercedes", "Audi", "Lada", "Kia", "Hyundai", "Ford", "Volvo", "Nissan" };
        _cars = new List<Car>();
        for (int i = 0; i < 15; i++)
        {
            var client = _clients[i % _clients.Count];
            var car = new Car(
                i + 1,
                $"{faker.Random.Char('А', 'Я')}{faker.Random.Number(100, 999)}{faker.Random.Char('А', 'Я')}{faker.Random.Char('А', 'Я')}{faker.Random.Number(10, 99)}",
                carBrands[i % carBrands.Length],
                client.Id
            );
            _cars.Add(car);
            client.AddCar(car);
        }

        // Генерация заказов
        _orders = new List<Order>();
        var now = DateTime.Now;
        int orderId = 1;

        foreach (var client in _clients)
        {
            var clientCars = _cars.Where(c => c.ClientId == client.Id).ToList();
            if (clientCars.Count == 0) continue;

            int orderCount = faker.Random.Number(1, 5);
            for (int i = 0; i < orderCount; i++)
            {
                var service = _services[faker.Random.Number(0, _services.Count - 1)];
                var car = clientCars[faker.Random.Number(0, clientCars.Count - 1)];
                var boxNumber = faker.Random.Number(1, 5);
                var startTime = now.AddMinutes(faker.Random.Number(-180, 60));

                var order = new Order(orderId++, client.Id, service.Id, car.Id, startTime, boxNumber);
                _orders.Add(order);
            }
        }

        _repository = new InMemoryOrderRepository(_clients, _services, _cars, _orders);
        _service = new CarWashService(_repository);
    }

    /// <summary>
    /// Тест 1: Вывести топ 5 клиентов по количеству посещений.
    /// </summary>
    [Fact]
    public void GetTop5ClientsByVisits_ReturnsTop5Clients()
    {
        // Act
        var result = _service.GetTop5ClientsByVisits();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count <= 5);
        for (int i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].VisitCount >= result[i + 1].VisitCount);
        }
    }

    /// <summary>
    /// Тест 2: Вывести информацию об автомобилях, находящихся на мойке в данный момент.
    /// </summary>
    [Fact]
    public void GetCarsCurrentlyAtWash_ReturnsActiveCars()
    {
        // Arrange
        var fixedTime = DateTime.Now;

        // Act
        var result = _service.GetCarsCurrentlyAtWash(fixedTime);

        // Assert
        Assert.NotNull(result);
        foreach (var car in result)
        {
            var isActive = _orders.Any(o =>
                o.CarId == car.Id &&
                fixedTime >= o.StartTime &&
                fixedTime < o.StartTime.AddMinutes(o.Service!.DurationMinutes));
            Assert.True(isActive);
        }
    }

    /// <summary>
    /// Тест 3: Вывести топ 5 наиболее популярных услуг.
    /// </summary>
    [Fact]
    public void GetTop5PopularServices_ReturnsTop5Services()
    {
        // Act
        var result = _service.GetTop5PopularServices();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count <= 5);
        for (int i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].OrderCount >= result[i + 1].OrderCount);
        }
    }

    /// <summary>
    /// Тест 4: Узнать для выбранного бокса мойки, начиная с какого времени он освобождается.
    /// </summary>
    [Fact]
    public void GetBoxFreeTime_ReturnsFreeTimeForBox()
    {
        // Arrange
        var fixedTime = DateTime.Now;
        var boxNumber = 1;

        // Act
        var result = _service.GetBoxFreeTime(boxNumber, fixedTime);

        // Assert
        if (result.HasValue)
        {
            Assert.True(result.Value > fixedTime);
        }
    }

    /// <summary>
    /// Тест 5: Для каждой услуги вывести суммарную выручку.
    /// </summary>
    [Fact]
    public void GetRevenuePerService_ReturnsRevenueForAllServices()
    {
        // Act
        var result = _service.GetRevenuePerService();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count > 0);
        foreach (var item in result)
        {
            var orderCount = _orders.Count(o => o.ServiceId == item.Service.Id);
            var expectedRevenue = item.Service.Cost * orderCount;
            Assert.Equal(expectedRevenue, item.TotalRevenue);
        }
    }
}