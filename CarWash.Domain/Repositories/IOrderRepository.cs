using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Интерфейс репозитория заказов.
/// </summary>
public interface IOrderRepository
{
    IReadOnlyList<Order> GetAllOrders();
    IReadOnlyList<Client> GetAllClients();
    IReadOnlyList<Service> GetAllServices();
    IReadOnlyList<Car> GetAllCars();
}