using CarWash.Contracts;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Domain.Models;

namespace CarWash.Application.Mapping;

/// <summary>
/// Преобразования между доменными сущностями и контрактами API
/// </summary>
public static class CarWashMapper
{
    /// <summary>
    /// Преобразовать автомобиль в ответ API
    /// </summary>
    public static CarResponse ToResponse(this Car car) => new()
    {
        Id = car.Id, LicensePlate = car.LicensePlate, Brand = car.Brand, CustomerId = car.Customer.Id
    };

    /// <summary>
    /// Преобразовать клиента в ответ API
    /// </summary>
    public static CustomerResponse ToResponse(this Customer customer) => new()
    {
        Id = customer.Id, FullName = customer.FullName, Phone = customer.Phone
    };

    /// <summary>
    /// Преобразовать услугу в ответ API
    /// </summary>
    public static WashServiceResponse ToResponse(this WashService service) => new()
    {
        Id = service.Id,
        Name = service.Name,
        Category = (CarCategoryDto)service.Category,
        Cost = service.Cost,
        DurationMinutes = service.DurationMinutes
    };

    /// <summary>
    /// Преобразовать заказ в ответ API
    /// </summary>
    public static OrderResponse ToResponse(this Order order) => new()
    {
        Id = order.Id,
        CustomerId = order.Customer.Id,
        CarId = order.Car.Id,
        WashServiceId = order.WashService.Id,
        StartTime = order.StartTime,
        EndTime = order.EndTime,
        BoxNumber = order.BoxNumber,
        IsInProgress = order.IsInProgress
    };

    /// <summary>
    /// Создать автомобиль из запроса
    /// </summary>
    public static Car ToDomain(this CarRequest request, Customer customer) => new()
    {
        Id = 0, LicensePlate = request.LicensePlate, Brand = request.Brand, Customer = customer
    };

    /// <summary>
    /// Создать клиента из запроса
    /// </summary>
    public static Customer ToDomain(this CustomerRequest request) => new()
    {
        Id = 0, FullName = request.FullName, Phone = request.Phone
    };

    /// <summary>
    /// Создать услугу из запроса
    /// </summary>
    public static WashService ToDomain(this WashServiceRequest request) => new()
    {
        Id = 0,
        Name = request.Name,
        Category = (CarCategory)request.Category,
        Cost = request.Cost,
        DurationMinutes = request.DurationMinutes
    };

    /// <summary>
    /// Создать заказ из запроса и найденных связанных сущностей
    /// </summary>
    public static Order ToDomain(this OrderRequest request, Customer customer, Car car, WashService service) => new()
    {
        Id = 0,
        Customer = customer,
        Car = car,
        WashService = service,
        StartTime = request.StartTime.Kind == DateTimeKind.Utc
            ? request.StartTime.ToLocalTime()
            : request.StartTime,
        BoxNumber = request.BoxNumber
    };

    /// <summary>
    /// Обновить поля клиента
    /// </summary>
    public static void ApplyTo(this CustomerRequest request, Customer customer)
    {
        customer.FullName = request.FullName;
        customer.Phone = request.Phone;
    }

    /// <summary>
    /// Обновить поля услуги
    /// </summary>
    public static void ApplyTo(this WashServiceRequest request, WashService service)
    {
        service.Name = request.Name;
        service.Category = (CarCategory)request.Category;
        service.Cost = request.Cost;
        service.DurationMinutes = request.DurationMinutes;
    }
}
