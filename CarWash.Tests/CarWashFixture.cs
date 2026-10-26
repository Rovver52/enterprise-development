using CarWash.Domain.Data;
using CarWash.Domain.Repositories;
using CarWash.Infrastructure.Repositories;

namespace CarWash.Tests;

/// <summary>
/// Общие зависимости тестов аналитических запросов
/// </summary>
public class CarWashFixture
{
    /// <summary>
    /// Начальные данные из доменного проекта
    /// </summary>
    public CarWashData Data { get; } = new();

    /// <summary>
    /// Репозиторий клиентов
    /// </summary>
    public ICustomerRepository CustomerRepository { get; }

    /// <summary>
    /// Репозиторий заказов
    /// </summary>
    public IOrderRepository OrderRepository { get; }

    /// <summary>
    /// Репозиторий услуг
    /// </summary>
    public IWashServiceRepository WashServiceRepository { get; }

    /// <summary>
    /// Создать репозитории с общими начальными данными
    /// </summary>
    public CarWashFixture()
    {
        OrderRepository = new InMemoryOrderRepository(Data);
        CustomerRepository = new InMemoryCustomerRepository(Data, OrderRepository, new InMemoryCarRepository(Data));
        WashServiceRepository = new InMemoryWashServiceRepository(Data, OrderRepository);
    }
}
