using Xunit;

namespace CarWash.Tests;

/// <summary>
/// Тесты операций автомойки
/// </summary>
public class CarWashTests(CarWashFixture fixture)
    : IClassFixture<CarWashFixture>
{
    /// <summary>
    /// Проверка пяти клиентов с наибольшим количеством посещений
    /// </summary>
    [Fact]
    public void GetTop5CustomersByVisits()
    {
        var result =
            fixture.CustomerRepository.GetTop5CustomersByVisits();

        var actual = result
            .Select(item => item.Customer.Id)
            .ToList();

        var expected = new[]
        {
            fixture.Data.Customers[0].Id,
            fixture.Data.Customers[1].Id,
            fixture.Data.Customers[2].Id,
            fixture.Data.Customers[3].Id,
            fixture.Data.Customers[4].Id
        };

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Проверка автомобилей, находящихся сейчас на мойке
    /// </summary>
    [Fact]
    public void GetCarsCurrentlyAtWash()
    {
        var result =
            fixture.OrderRepository.GetCarsCurrentlyAtWash();

        Assert.Single(result);

        Assert.Contains(
            result,
            car => car.Id == fixture.Data.Cars[0].Id);
    }

    /// <summary>
    /// Проверка пяти наиболее популярных услуг
    /// </summary>
    [Fact]
    public void GetTop5PopularServices()
    {
        var result =
            fixture.WashServiceRepository.GetTop5PopularServices();

        var actual = result
            .Select(item => item.WashService.Id)
            .ToList();

        var expected = new[]
        {
            1,
            2,
            3,
            4,
            5
        };

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Проверка времени освобождения бокса
    /// </summary>
    [Fact]
    public void GetBoxAvailableFrom()
    {
        var result =
            fixture.OrderRepository.GetBoxAvailableFrom(1);

        Assert.Equal(fixture.Data.Orders[15].EndTime, result);
    }

    /// <summary>
    /// Проверка суммарной выручки по услугам
    /// </summary>
    [Fact]
    public void GetRevenuePerService()
    {
        var result =
            fixture.WashServiceRepository.GetRevenuePerService();

        Assert.Contains(
            result,
            item =>
                item.WashService.Id == fixture.Data.WashServices[0].Id &&
                item.Revenue == 3000m);
    }
}