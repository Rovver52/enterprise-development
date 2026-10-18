using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Базовые операции с коллекцией сущностей
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Получить все сущности
    /// </summary>
    IReadOnlyList<TEntity> GetAll();

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    TEntity? GetById(int id);

    /// <summary>
    /// Добавить сущность и назначить свободный идентификатор
    /// </summary>
    void Add(TEntity entity);

    /// <summary>
    /// Обновить существующую сущность
    /// </summary>
    void Update(TEntity entity);

    /// <summary>
    /// Удалить сущность
    /// </summary>
    void Delete(int id);
}
