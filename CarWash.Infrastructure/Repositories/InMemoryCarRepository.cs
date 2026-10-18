using CarWash.Domain.Data;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий автомобилей в памяти
/// </summary>
public class InMemoryCarRepository(CarWashData data)
    : InMemoryRepository<Car>(data.Cars), ICarRepository;
