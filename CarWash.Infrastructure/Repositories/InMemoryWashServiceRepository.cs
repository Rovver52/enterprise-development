using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий услуг автомойки в памяти
/// </summary>
public class InMemoryWashServiceRepository(List<WashService> washServices)
    : IWashServiceRepository
{
    /// <summary>
    /// Получить все услуги автомойки
    /// </summary>
    public IReadOnlyList<WashService> GetAll() =>
        washServices.AsReadOnly();

    /// <summary>
    /// Получить услугу автомойки по идентификатору
    /// </summary>
    public WashService? GetById(int id) =>
        washServices.FirstOrDefault(washService =>
            washService.Id == id);

    /// <summary>
    /// Добавить услугу автомойки
    /// </summary>
    public void Add(WashService washService) =>
        washServices.Add(washService);

    /// <summary>
    /// Обновить услугу автомойки
    /// </summary>
    public void Update(WashService washService)
    {
        var index = washServices.FindIndex(
            item => item.Id == washService.Id);

        if (index >= 0)
            washServices[index] = washService;
    }

    /// <summary>
    /// Удалить услугу автомойки
    /// </summary>
    public void Delete(int id) =>
        washServices.RemoveAll(washService =>
            washService.Id == id);
}