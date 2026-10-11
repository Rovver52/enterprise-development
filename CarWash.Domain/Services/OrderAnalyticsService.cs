using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Domain.Services;

/// <summary>
/// Операции анализа заказов
/// </summary>
public class OrderAnalyticsService(IOrderRepository orderRepository)
{
    /// <summary>
    /// Получить автомобили, которые сейчас находятся на мойке
    /// </summary>
    public IReadOnlyList<Car> GetCarsCurrentlyAtWash()
    {
        return orderRepository.GetAll()
            .Where(order => order.IsInProgress)
            .Select(order => order.Car)
            .DistinctBy(car => car.Id)
            .ToList();
    }

    /// <summary>
    /// Определить время освобождения выбранного бокса
    /// </summary>
    public DateTime GetBoxAvailableFrom(int boxNumber)
    {
        var currentOrder = orderRepository.GetAll()
            .Where(order =>
                order.BoxNumber == boxNumber &&
                order.IsInProgress)
            .OrderBy(order => order.EndTime)
            .FirstOrDefault();

        return currentOrder?.EndTime ?? DateTime.Now;
    }
}