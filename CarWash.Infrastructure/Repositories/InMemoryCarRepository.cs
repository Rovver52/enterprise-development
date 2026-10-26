using CarWash.Domain.Data;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий автомобилей в памяти
/// </summary>
public class InMemoryCarRepository(CarWashData data)
    : InMemoryRepository<Car>(data.Cars), ICarRepository
{
    /// <inheritdoc />
    public IReadOnlyList<Car> GetByCustomerId(int customerId) =>
        Items.Values.Where(car => car.Customer.Id == customerId).OrderBy(car => car.Id).ToList();

    /// <inheritdoc />
    public Customer? GetCustomer(int carId) => GetById(carId)?.Customer;

    /// <inheritdoc />
    public override void Add(Car entity)
    {
        base.Add(entity);
        entity.Customer.Cars = [.. entity.Customer.Cars, entity];
    }

    /// <inheritdoc />
    public override void Update(Car entity)
    {
        var current = GetById(entity.Id);
        if (current is null)
            return;

        if (current.Customer.Id != entity.Customer.Id)
        {
            current.Customer.Cars = current.Customer.Cars.Where(car => car.Id != current.Id).ToList();
            entity.Customer.Cars = [.. entity.Customer.Cars, current];
        }

        current.LicensePlate = entity.LicensePlate;
        current.Brand = entity.Brand;
        current.Customer = entity.Customer;
    }

    /// <inheritdoc />
    public override void Delete(int id)
    {
        if (Items.TryRemove(id, out var car))
            car.Customer.Cars = car.Customer.Cars.Where(item => item.Id != id).ToList();
    }
}
