using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using CarWash.Domain.Services;
using CarWash.Domain.Services.Queries;
using CarWash.Domain.Services.QueryHandlers;
using CarWash.Infrastructure.Repositories;
using Xunit;

namespace CarWash.Tests;

/// <summary>
/// Фикстура с тестовыми данными.
/// </summary>
public class CarWashTestFixture
{
    public InMemoryOrderContext OrderContext { get; }
    public InMemoryCustomerContext CustomerContext { get; }
    public InMemoryWashServiceContext ServiceContext { get; }
    public InMemoryCarContext CarContext { get; }
    public CarWashService Service { get; }

    public CarWashTestFixture()
    {
        OrderContext = new InMemoryOrderContext();
        CustomerContext = new InMemoryCustomerContext();
        ServiceContext = new InMemoryWashServiceContext();
        CarContext = new InMemoryCarContext();

        // Статические данные - услуги
        var services = new List<WashService>
        {
            new() { Id = 1, Name = "Экспресс-мойка", Category = CarCategory.Sedan, Cost = 500, DurationMinutes = 15 },
            new() { Id = 2, Name = "Комплексная мойка", Category = CarCategory.Sedan, Cost = 1200, DurationMinutes = 40 },
            new() { Id = 3, Name = "Мойка двигателя", Category = CarCategory.Sedan, Cost = 800, DurationMinutes = 30 },
            new() { Id = 4, Name = "Химчистка салона", Category = CarCategory.Sedan, Cost = 3000, DurationMinutes = 120 },
            new() { Id = 5, Name = "Мойка грузовика", Category = CarCategory.Truck, Cost = 2000, DurationMinutes = 60 }
        };

        foreach (var service in services)
            ServiceContext.Add(service);

        // Статические данные - клиенты
        var customers = new List<Customer>
        {
            new() { Id = 1, FullName = "Иванов Иван Иванович", Phone = "+7 (900) 123-45-67" },
            new() { Id = 2, FullName = "Петров Петр Петрович", Phone = "+7 (900) 234-56-78" },
            new() { Id = 3, FullName = "Сидоров Сидор Сидорович", Phone = "+7 (900) 345-67-89" },
            new() { Id = 4, FullName = "Козлов Алексей Дмитриевич", Phone = "+7 (900) 456-78-90" },
            new() { Id = 5, FullName = "Новикова Мария Сергеевна", Phone = "+7 (900) 567-89-01" }
        };

        foreach (var customer in customers)
            CustomerContext.Add(customer);

        // Статические данные - автомобили
        var cars = new List<Car>
        {
            new() { Id = 1, LicensePlate = "А123БВ77", Brand = "Toyota", CustomerId = 1 },
            new() { Id = 2, LicensePlate = "Б456ГД77", Brand = "BMW", CustomerId = 1 },
            new() { Id = 3, LicensePlate = "В789ЕЗ77", Brand = "Mercedes", CustomerId = 2 },
            new() { Id = 4, LicensePlate = "Г012ЖИ77", Brand = "Audi", CustomerId = 2 },
            new() { Id = 5, LicensePlate = "Д345КЛ77", Brand = "Lada", CustomerId = 3 },
            new() { Id = 6, LicensePlate = "Е678МН77", Brand = "Kia", CustomerId = 3 },
            new() { Id = 7, LicensePlate = "Ж901ОП77", Brand = "Hyundai", CustomerId = 4 },
            new() { Id = 8, LicensePlate = "З234РС77", Brand = "Ford", CustomerId = 4 },
            new() { Id = 9, LicensePlate = "И567ТУ77", Brand = "Volvo", CustomerId = 5 },
            new() { Id = 10, LicensePlate = "К890ФХ77", Brand = "Nissan", CustomerId = 5 }
        };

        foreach (var car in cars)
            CarContext.Add(car);

        // Статические данные - заказы
        // ВАЖНО: Используем DateTime.Now и обязательно передаем свойство Service, 
        // чтобы Order.EndTime вычислился корректно (StartTime + DurationMinutes)
        var now = DateTime.Now;
        var orders = new List<Order>
        {
            // Активные заказы (Started recently, duration makes them still active)
            new() { Id = 1, CustomerId = 1, ServiceId = 2, CarId = 1, StartTime = now.AddMinutes(-30), BoxNumber = 1, Service = services.First(s => s.Id == 2) },
            new() { Id = 8, CustomerId = 3, ServiceId = 1, CarId = 5, StartTime = now.AddMinutes(-10), BoxNumber = 1, Service = services.First(s => s.Id == 1) },

            // Завершённые заказы (для проверки сортировки и выручки)
            new() { Id = 2, CustomerId = 1, ServiceId = 1, CarId = 1, StartTime = now.AddMinutes(-120), BoxNumber = 2, Service = services.First(s => s.Id == 1) },
            new() { Id = 3, CustomerId = 1, ServiceId = 1, CarId = 2, StartTime = now.AddMinutes(-200), BoxNumber = 1, Service = services.First(s => s.Id == 1) },
            new() { Id = 4, CustomerId = 1, ServiceId = 3, CarId = 2, StartTime = now.AddMinutes(-300), BoxNumber = 3, Service = services.First(s => s.Id == 3) },
            new() { Id = 5, CustomerId = 2, ServiceId = 2, CarId = 3, StartTime = now.AddMinutes(-60), BoxNumber = 2, Service = services.First(s => s.Id == 2) },
            new() { Id = 6, CustomerId = 2, ServiceId = 1, CarId = 4, StartTime = now.AddMinutes(-150), BoxNumber = 1, Service = services.First(s => s.Id == 1) },
            new() { Id = 7, CustomerId = 2, ServiceId = 4, CarId = 3, StartTime = now.AddMinutes(-400), BoxNumber = 4, Service = services.First(s => s.Id == 4) },
            new() { Id = 9, CustomerId = 3, ServiceId = 2, CarId = 6, StartTime = now.AddMinutes(-180), BoxNumber = 2, Service = services.First(s => s.Id == 2) },
            new() { Id = 10, CustomerId = 3, ServiceId = 5, CarId = 5, StartTime = now.AddMinutes(-500), BoxNumber = 5, Service = services.First(s => s.Id == 5) },
            new() { Id = 11, CustomerId = 4, ServiceId = 1, CarId = 7, StartTime = now.AddMinutes(-90), BoxNumber = 3, Service = services.First(s => s.Id == 1) },
            new() { Id = 12, CustomerId = 4, ServiceId = 3, CarId = 8, StartTime = now.AddMinutes(-250), BoxNumber = 3, Service = services.First(s => s.Id == 3) },
            new() { Id = 13, CustomerId = 5, ServiceId = 2, CarId = 9, StartTime = now.AddMinutes(-100), BoxNumber = 4, Service = services.First(s => s.Id == 2) }
        };

        foreach (var order in orders)
            OrderContext.Add(order);

        // Создаём обработчики запросов
        var topClientsQuery = new TopClientsQueryHandler(OrderContext, CustomerContext);
        var popularServicesQuery = new PopularServicesQueryHandler(OrderContext, ServiceContext);
        var revenueQuery = new RevenueQueryHandler(OrderContext, ServiceContext);
        var activeCarsQuery = new ActiveCarsQueryHandler(OrderContext, CarContext);
        var boxAvailabilityQuery = new BoxAvailabilityQueryHandler(OrderContext);

        // Создаём сервис
        Service = new CarWashService(
            topClientsQuery,
            popularServicesQuery,
            revenueQuery,
            activeCarsQuery,
            boxAvailabilityQuery);
    }
}

/// <summary>
/// Тесты сервиса CarWashService.
/// </summary>
public class CarWashServiceTests(CarWashTestFixture fixture) : IClassFixture<CarWashTestFixture>
{
    private readonly CarWashService _service = fixture.Service;
    private readonly InMemoryOrderContext _orderContext = fixture.OrderContext;

    /// <summary>
    /// Тест 1: GetTop5ClientsByVisits должен возвращать топ-5 клиентов, отсортированных по убыванию количества посещений.
    /// </summary>
    [Fact]
    public void GetTop5ClientsByVisitsShouldReturnTop5ClientsSortedByVisitCount()
    {
        // Act
        var result = _service.GetTop5ClientsByVisits();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count <= 5);
        
        // Проверяем, что отсортировано по убыванию
        var expected = result.OrderByDescending(x => x.VisitCount).ToList();
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Тест 2: GetCarsCurrentlyAtWash должен возвращать автомобили, находящиеся на мойке в данный момент.
    /// </summary>
    [Fact]
    public void GetCarsCurrentlyAtWashShouldReturnActiveCars()
    {
        // Act (метод внутри использует DateTime.Now, поэтому переменная fixedTime здесь не нужна)
        var result = _service.GetCarsCurrentlyAtWash();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result); // Гарантированно не пусто, т.к. в фикстуре есть активные заказы
    }

    /// <summary>
    /// Тест 3: GetTop5PopularServices должен возвращать топ-5 услуг, отсортированных по убыванию популярности.
    /// </summary>
    [Fact]
    public void GetTop5PopularServicesShouldReturnTop5ServicesSortedByOrderCount()
    {
        // Act
        var result = _service.GetTop5PopularServices();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count <= 5);
        
        // Проверяем, что отсортировано по убыванию
        var expected = result.OrderByDescending(x => x.OrderCount).ToList();
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Тест 4: GetBoxFreeTime должен возвращать время освобождения бокса для занятого бокса.
    /// </summary>
    [Fact]
    public void GetBoxFreeTimeShouldReturnFreeTimeForBusyBox()
    {
        // Arrange
        var fixedTime = DateTime.Now;
        var boxNumber = 1;

        // Act
        var result = _service.GetBoxFreeTime(boxNumber, fixedTime);

        // Assert
        Assert.True(result.HasValue);
        Assert.True(result.Value > fixedTime);
    }

    /// <summary>
    /// Тест 5: GetRevenuePerService должен возвращать корректную выручку по каждой услуге.
    /// </summary>
    [Fact]
    public void GetRevenuePerServiceShouldReturnCorrectRevenue()
    {
        // Act
        var result = _service.GetRevenuePerService();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count > 0);
        
        foreach (var item in result)
        {
            var orderCount = _orderContext.GetAll().Count(o => o.ServiceId == item.Service.Id);
            var expectedRevenue = item.Service.Cost * orderCount;
            Assert.Equal(expectedRevenue, item.TotalRevenue);
        }
    }
}