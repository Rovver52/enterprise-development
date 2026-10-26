using CarWash.Application.Exceptions;
using CarWash.Application.Mapping;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Операции приложения с клиентами
/// </summary>
public class CustomerService(ICustomerRepository customers, IOrderRepository orders) : ICustomerService
{
    /// <inheritdoc />
    public IReadOnlyList<CustomerResponse> GetAll() =>
        customers.GetAll().Select(customer => customer.ToResponse()).ToList();

    /// <inheritdoc />
    public CustomerResponse GetById(int id) =>
        (customers.GetById(id) ?? throw new EntityNotFoundException("Клиент не найден.")).ToResponse();

    /// <inheritdoc />
    public CustomerResponse Create(CustomerRequest request)
    {
        var customer = request.ToDomain();
        customers.Add(customer);
        return customer.ToResponse();
    }

    /// <inheritdoc />
    public void Update(int id, CustomerRequest request)
    {
        var customer = customers.GetById(id) ?? throw new EntityNotFoundException("Клиент не найден.");
        request.ApplyTo(customer);
        customers.Update(customer);
    }

    /// <inheritdoc />
    public void Delete(int id)
    {
        _ = GetById(id);
        if (orders.GetByCustomerId(id).Count != 0)
            throw new EntityConflictException("Клиент используется в заказах.");
        if (customers.GetCars(id).Count != 0)
            throw new EntityConflictException("У клиента есть автомобили. Сначала смените их владельца или удалите автомобили.");
        customers.Delete(id);
    }

    /// <inheritdoc />
    public IReadOnlyList<CarResponse> GetCars(int id)
    {
        _ = GetById(id);
        return customers.GetCars(id).Select(car => car.ToResponse()).ToList();
    }

    /// <inheritdoc />
    public IReadOnlyList<OrderResponse> GetOrders(int id)
    {
        _ = GetById(id);
        return orders.GetByCustomerId(id).Select(order => order.ToResponse()).ToList();
    }
}
