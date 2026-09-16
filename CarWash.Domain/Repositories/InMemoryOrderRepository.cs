using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий в памяти с тестовыми данными.
/// </summary>
public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Client> _clients;
    private readonly List<Service> _services;
    private readonly List<Car> _cars;
    private readonly List<Order> _orders;

    public InMemoryOrderRepository(
        List<Client> clients,
        List<Service> services,
        List<Car> cars,
        List<Order> orders)
    {
        _clients = clients;
        _services = services;
        _cars = cars;
        _orders = orders;

        // Навигационные свойства
        foreach (var order in _orders)
        {
            order.Client = _clients.FirstOrDefault(c => c.Id == order.ClientId);
            order.Service = _services.FirstOrDefault(s => s.Id == order.ServiceId);
            order.Car = _cars.FirstOrDefault(c => c.Id == order.CarId);
        }

        foreach (var car in _cars)
        {
            car.Client = _clients.FirstOrDefault(c => c.Id == car.ClientId);
        }
    }

    public IReadOnlyList<Order> GetAllOrders() => _orders.AsReadOnly();
    public IReadOnlyList<Client> GetAllClients() => _clients.AsReadOnly();
    public IReadOnlyList<Service> GetAllServices() => _services.AsReadOnly();
    public IReadOnlyList<Car> GetAllCars() => _cars.AsReadOnly();
}