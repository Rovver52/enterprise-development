using CarWash.Domain.Models;
using CarWash.Domain.Services;
using CarWash.Infrastructure.Repositories;

namespace CarWash.Tests;

/// <summary>
/// Данные для тестов
/// </summary>
public class CarWashFixture
{
    /// <summary>
    /// Сервис аналитики клиентов
    /// </summary>
    public CustomerAnalyticsService CustomerAnalyticsService { get; }

    /// <summary>
    /// Сервис аналитики заказов
    /// </summary>
    public OrderAnalyticsService OrderAnalyticsService { get; }

    /// <summary>
    /// Сервис аналитики услуг
    /// </summary>
    public WashServiceAnalyticsService WashServiceAnalyticsService { get; }

    /// <summary>
    /// Первый клиент
    /// </summary>
    public Customer Customer1 { get; }

    /// <summary>
    /// Второй клиент
    /// </summary>
    public Customer Customer2 { get; }

    /// <summary>
    /// Третий клиент
    /// </summary>
    public Customer Customer3 { get; }

    /// <summary>
    /// Четвертый клиент
    /// </summary>
    public Customer Customer4 { get; }

    /// <summary>
    /// Пятый клиент
    /// </summary>
    public Customer Customer5 { get; }

    /// <summary>
    /// Первый автомобиль
    /// </summary>
    public Car Car1 { get; }

    /// <summary>
    /// Первая услуга
    /// </summary>
    public WashService WashService1 { get; }

    /// <summary>
    /// Текущий заказ
    /// </summary>
    public Order CurrentOrder { get; }

    /// <summary>
    /// Создает данные и зависимости для выполнения тестов
    /// </summary>
    public CarWashFixture()
    {
        Car1 = new Car
        {
            Id = 1,
            LicensePlate = "A001AA",
            Brand = "Toyota"
        };

        var car2 = new Car
        {
            Id = 2,
            LicensePlate = "A002AA",
            Brand = "Kia"
        };

        var car3 = new Car
        {
            Id = 3,
            LicensePlate = "A003AA",
            Brand = "Lada"
        };

        var car4 = new Car
        {
            Id = 4,
            LicensePlate = "A004AA",
            Brand = "BMW"
        };

        var car5 = new Car
        {
            Id = 5,
            LicensePlate = "A005AA",
            Brand = "Audi"
        };

        var car6 = new Car
        {
            Id = 6,
            LicensePlate = "A006AA",
            Brand = "Ford"
        };

        Customer1 = new Customer
        {
            Id = 1,
            FullName = "Иванов Иван Иванович",
            Phone = "79000000001",
            Cars = [Car1]
        };

        Customer2 = new Customer
        {
            Id = 2,
            FullName = "Петров Петр Петрович",
            Phone = "79000000002",
            Cars = [car2]
        };

        Customer3 = new Customer
        {
            Id = 3,
            FullName = "Сидоров Алексей Олегович",
            Phone = "79000000003",
            Cars = [car3]
        };

        Customer4 = new Customer
        {
            Id = 4,
            FullName = "Смирнов Максим Игоревич",
            Phone = "79000000004",
            Cars = [car4]
        };

        Customer5 = new Customer
        {
            Id = 5,
            FullName = "Кузнецов Андрей Сергеевич",
            Phone = "79000000005",
            Cars = [car5]
        };

        var customer6 = new Customer
        {
            Id = 6,
            FullName = "Попов Дмитрий Андреевич",
            Phone = "79000000006",
            Cars = [car6]
        };

        WashService1 = new WashService
        {
            Id = 1,
            Name = "Стандартная мойка",
            Category = CarCategory.Sedan,
            Cost = 500m,
            DurationMinutes = 30
        };

        var washService2 = new WashService
        {
            Id = 2,
            Name = "Мойка кузова",
            Category = CarCategory.Sedan,
            Cost = 400m,
            DurationMinutes = 20
        };

        var washService3 = new WashService
        {
            Id = 3,
            Name = "Комплексная мойка",
            Category = CarCategory.Sedan,
            Cost = 800m,
            DurationMinutes = 40
        };

        var washService4 = new WashService
        {
            Id = 4,
            Name = "Мойка внедорожника",
            Category = CarCategory.SUV,
            Cost = 1000m,
            DurationMinutes = 45
        };

        var washService5 = new WashService
        {
            Id = 5,
            Name = "Мойка грузовика",
            Category = CarCategory.Truck,
            Cost = 1500m,
            DurationMinutes = 60
        };

        var washService6 = new WashService
        {
            Id = 6,
            Name = "Мойка мотоцикла",
            Category = CarCategory.Motorcycle,
            Cost = 300m,
            DurationMinutes = 15
        };

        var customers = new List<Customer>
        {
            Customer1,
            Customer2,
            Customer3,
            Customer4,
            Customer5,
            customer6
        };

        var washServices = new List<WashService>
        {
            WashService1,
            washService2,
            washService3,
            washService4,
            washService5,
            washService6
        };

        CurrentOrder = new Order
        {
            Id = 16,
            Customer = Customer1,
            Car = Car1,
            WashService = WashService1,
            StartTime = DateTime.Now.AddMinutes(-5),
            BoxNumber = 1
        };

        var orders = new List<Order>
        {
            CreatePastOrder(1, Customer1, Car1, WashService1, 1),
            CreatePastOrder(2, Customer1, Car1, WashService1, 1),
            CreatePastOrder(3, Customer1, Car1, WashService1, 1),
            CreatePastOrder(4, Customer1, Car1, WashService1, 1),
            CreatePastOrder(5, Customer1, Car1, WashService1, 1),

            CreatePastOrder(6, Customer2, car2, washService2, 2),
            CreatePastOrder(7, Customer2, car2, washService2, 2),
            CreatePastOrder(8, Customer2, car2, washService2, 2),
            CreatePastOrder(9, Customer2, car2, washService2, 2),

            CreatePastOrder(10, Customer3, car3, washService3, 3),
            CreatePastOrder(11, Customer3, car3, washService3, 3),
            CreatePastOrder(12, Customer3, car3, washService3, 3),

            CreatePastOrder(13, Customer4, car4, washService4, 4),
            CreatePastOrder(14, Customer4, car4, washService4, 4),

            CreatePastOrder(15, Customer5, car5, washService5, 5),

            CurrentOrder
        };

        var customerRepository =
            new InMemoryCustomerRepository(customers);

        var washServiceRepository =
            new InMemoryWashServiceRepository(washServices);

        var orderRepository =
            new InMemoryOrderRepository(orders);

        CustomerAnalyticsService =
            new CustomerAnalyticsService(
                customerRepository,
                orderRepository);

        OrderAnalyticsService =
            new OrderAnalyticsService(orderRepository);

        WashServiceAnalyticsService =
            new WashServiceAnalyticsService(
                washServiceRepository,
                orderRepository);
    }

    private static Order CreatePastOrder(
        int id,
        Customer customer,
        Car car,
        WashService washService,
        int boxNumber)
    {
        return new Order
        {
            Id = id,
            Customer = customer,
            Car = car,
            WashService = washService,
            StartTime = DateTime.Now.AddDays(-1),
            BoxNumber = boxNumber
        };
    }
}