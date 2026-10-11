using System.Text.Json.Serialization;
using CarWash.Domain.Repositories;
using CarWash.Domain.Services;
using CarWash.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Один экземпляр каждого репозитория хранит данные между HTTP-запросами.
builder.Services.AddSingleton<ICarRepository>(_ => new InMemoryCarRepository([]));
builder.Services.AddSingleton<ICustomerRepository>(_ => new InMemoryCustomerRepository([]));
builder.Services.AddSingleton<IWashServiceRepository>(_ => new InMemoryWashServiceRepository([]));
builder.Services.AddSingleton<IOrderRepository>(_ => new InMemoryOrderRepository([]));

builder.Services.AddScoped<CustomerAnalyticsService>();
builder.Services.AddScoped<OrderAnalyticsService>();
builder.Services.AddScoped<WashServiceAnalyticsService>();

var app = builder.Build();

// List<T> в репозиториях первой лабы не поддерживает параллельное изменение.
// Последовательная обработка защищает чтение, запись и сериализацию ответов.
using var requestLock = new SemaphoreSlim(1, 1);
app.Use(async (context, next) =>
{
    await requestLock.WaitAsync(context.RequestAborted);
    try
    {
        await next(context);
    }
    finally
    {
        requestLock.Release();
    }
});

app.MapControllers();
app.Run();
