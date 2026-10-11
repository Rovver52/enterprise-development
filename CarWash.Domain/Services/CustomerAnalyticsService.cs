using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Domain.Services;

/// <summary>
/// Операции анализа клиентов
/// </summary>
public class CustomerAnalyticsService(
    ICustomerRepository customerRepository,
    IOrderRepository orderRepository)
{
    /// <summary>
    /// Получить пять клиентов с наибольшим количеством посещений
    /// </summary>
    public IReadOnlyList<(Customer Customer, int VisitCount)> GetTop5CustomersByVisits()
    {
        return customerRepository.GetAll()
            .Select(customer => (
                Customer: customer,
                VisitCount: orderRepository.GetAll()
                    .Count(order => order.Customer.Id == customer.Id)))
            .OrderByDescending(result => result.VisitCount)
            .Take(5)
            .ToList();
    }
}