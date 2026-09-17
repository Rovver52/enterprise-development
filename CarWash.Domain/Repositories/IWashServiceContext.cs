namespace CarWash.Domain.Repositories;

using CarWash.Domain.Models;

/// <summary>
/// Контекст для работы с услугами
/// </summary>
public interface IWashServiceContext
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
    void Add(WashService service);

    /// <summary>
    /// Обновить данные услуги
    /// </summary>
    void Update(WashService service);

    /// <summary>
    /// Удалить услугу по идентификатору
    /// </summary>
    void Delete(int id);
}