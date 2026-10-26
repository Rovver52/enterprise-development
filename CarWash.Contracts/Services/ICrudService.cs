namespace CarWash.Contracts.Services;

/// <summary>
/// Базовые операции приложения с сущностью
/// </summary>
/// <typeparam name="TRequest">Данные для создания и изменения</typeparam>
/// <typeparam name="TResponse">Представление сущности</typeparam>
public interface ICrudService<TRequest, TResponse>
{
    /// <summary>
    /// Получить список сущностей
    /// </summary>
    IReadOnlyList<TResponse> GetAll();

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    TResponse GetById(int id);

    /// <summary>
    /// Создать сущность
    /// </summary>
    TResponse Create(TRequest request);

    /// <summary>
    /// Изменить сущность
    /// </summary>
    void Update(int id, TRequest request);

    /// <summary>
    /// Удалить сущность
    /// </summary>
    void Delete(int id);
}
