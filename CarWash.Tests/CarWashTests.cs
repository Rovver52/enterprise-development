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
            fixture.CustomerAnalyticsService.GetTop5CustomersByVisits();

        var actual = result
            .Select(item => item.Customer.Id)
            .ToList();

        var expected = new[]
        {
            fixture.Customer1.Id,
            fixture.Customer2.Id,
            fixture.Customer3.Id,
            fixture.Customer4.Id,
            fixture.Customer5.Id
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
            fixture.OrderAnalyticsService.GetCarsCurrentlyAtWash();

        Assert.Single(result);

        Assert.Contains(
            result,
            car => car.Id == fixture.Car1.Id);
    }

    /// <summary>
    /// Проверка пяти наиболее популярных услуг
    /// </summary>
    [Fact]
    public void GetTop5PopularServices()
    {
        var result =
            fixture.WashServiceAnalyticsService.GetTop5PopularServices();

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
            fixture.OrderAnalyticsService.GetBoxAvailableFrom(1);

        Assert.Equal(fixture.CurrentOrder.EndTime, result);
    }

    /// <summary>
    /// Проверка суммарной выручки по услугам
    /// </summary>
    [Fact]
    public void GetRevenuePerService()
    {
        var result =
            fixture.WashServiceAnalyticsService.GetRevenuePerService();

        Assert.Contains(
            result,
            item =>
                item.WashService.Id == fixture.WashService1.Id &&
                item.Revenue == 3000m);
    }
}