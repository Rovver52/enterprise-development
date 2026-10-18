using System.Collections.Concurrent;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Базовый репозиторий сущностей в памяти
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public abstract class InMemoryRepository<TEntity>(IEnumerable<TEntity> entities)
    : IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Коллекция сущностей по идентификаторам
    /// </summary>
    protected ConcurrentDictionary<int, TEntity> Items { get; } =
        new(entities.ToDictionary(entity => entity.Id));

    /// <inheritdoc />
    public IReadOnlyList<TEntity> GetAll() =>
        Items.Values.OrderBy(entity => entity.Id).ToList();

    /// <inheritdoc />
    public TEntity? GetById(int id) =>
        Items.GetValueOrDefault(id);

    /// <inheritdoc />
    public void Add(TEntity entity)
    {
        var id = Items.Count + 1;
        while (true)
        {
            entity.Id = id;
            if (Items.TryAdd(id, entity))
                return;
            id = checked(id + 1);
        }
    }

    /// <inheritdoc />
    public void Update(TEntity entity)
    {
        if (Items.TryGetValue(entity.Id, out var current))
            Items.TryUpdate(entity.Id, entity, current);
    }

    /// <inheritdoc />
    public void Delete(int id) =>
        Items.TryRemove(id, out _);
}
