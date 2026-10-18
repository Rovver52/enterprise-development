using CarWash.Domain.Data;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий клиентов и их связей в памяти
/// </summary>
public class InMemoryCustomerRepository(CarWashData data, IOrderRepository orderRepository)
    : InMemoryRepository<Customer>(data.Customers), ICustomerRepository
{
    /// <inheritdoc />
    public IReadOnlyList<Car> GetCars(int customerId) =>
        GetById(customerId)?.Cars.ToList() ?? [];

    /// <inheritdoc />
    public IReadOnlyList<Customer> GetByCarId(int carId) =>
        Items.Values.Where(customer => customer.Cars.Any(car => car.Id == carId))
            .OrderBy(customer => customer.Id).ToList();

    /// <inheritdoc />
    public void AddCar(int customerId, Car car)
    {
        var customer = GetById(customerId);
        if (customer is not null && customer.Cars.All(item => item.Id != car.Id))
            customer.Cars = [.. customer.Cars, car];
    }

    /// <inheritdoc />
    public void RemoveCar(int customerId, int carId)
    {
        var customer = GetById(customerId);
        if (customer is not null)
            customer.Cars = customer.Cars.Where(car => car.Id != carId).ToList();
    }

    /// <inheritdoc />
    public void RemoveCarFromAll(int carId)
    {
        foreach (var customer in GetByCarId(carId))
            RemoveCar(customer.Id, carId);
    }

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
