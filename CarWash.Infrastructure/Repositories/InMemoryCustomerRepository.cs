using CarWash.Domain.Data;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий клиентов и их связей в памяти
/// </summary>
public class InMemoryCustomerRepository(CarWashData data, IOrderRepository orderRepository, ICarRepository carRepository)
    : InMemoryRepository<Customer>(data.Customers), ICustomerRepository
{
    /// <inheritdoc />
    public IReadOnlyList<Car> GetCars(int customerId) => carRepository.GetByCustomerId(customerId);

    /// <inheritdoc />
    public IReadOnlyList<(Customer Customer, int VisitCount)> GetTop5CustomersByVisits()
    {
        var visits = orderRepository.GetAll()
            .GroupBy(order => order.Customer.Id)
            .ToDictionary(group => group.Key, group => group.Count());
        return Items.Values
            .Select(customer => (Customer: customer, VisitCount: visits.GetValueOrDefault(customer.Id)))
            .OrderByDescending(item => item.VisitCount).ThenBy(item => item.Customer.Id)
            .Take(5).ToList();
    }
}
