using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий услуг автомойки
/// </summary>
public interface IWashServiceRepository
{
    /// <summary>
    /// Получить все услуги
    /// </summary>
    IReadOnlyList<WashService> GetAll();

    /// <summary>
    /// Получить услугу по идентификатору
    /// </summary>
    WashService? GetById(int id);

    /// <summary>
    /// Добавить услугу
    /// </summary>
    void Add(WashService washService);

    /// <summary>
    /// Обновить услугу
    /// </summary>
    void Update(WashService washService);

    /// <summary>
    /// Удалить услугу
    /// </summary>
    void Delete(int id);
}