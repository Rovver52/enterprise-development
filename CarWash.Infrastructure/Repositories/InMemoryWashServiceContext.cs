using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Контекст услуг в памяти.
/// </summary>
public class InMemoryWashServiceContext : IWashServiceContext
{
    private readonly List<WashService> _services = new();

    public IReadOnlyList<WashService> GetAll() => _services.AsReadOnly();
    public WashService? GetById(int id) => _services.FirstOrDefault(s => s.Id == id);
    public void Add(WashService service) => _services.Add(service);
    public void Update(WashService service)
    {
        var index = _services.FindIndex(s => s.Id == service.Id);
        if (index >= 0) _services[index] = service;
    }
    public void Delete(int id)
    {
        var service = _services.FirstOrDefault(s => s.Id == id);
        if (service != null) _services.Remove(service);
    }
}