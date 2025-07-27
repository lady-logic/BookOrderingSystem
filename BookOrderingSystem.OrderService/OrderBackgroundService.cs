using BookOrderingSystem.Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace BookOrderingSystem.OrderService;

public class OrderBackgroundService : BackgroundService
{
    private readonly IBus _bus;
    private readonly Random _random = new Random();
    private readonly string[] _bookTitles =
        {
        "Clean Code",
        "Design Patterns",
        "Refactoring",
        "The Pragmatic Programmer",
        "You Don't Know JS",
        "Effective C#",
        "Domain-Driven Design"
    };

    public OrderBackgroundService(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("OrderService gestartet!");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(5000, stoppingToken); 

            var order = GenerateRandomOrder();
            await _bus.Publish(order);
            Console.WriteLine($"Bestellung gesendet: {order.BookTitle}...");
        }
    }
    private BookOrderSubmitted GenerateRandomOrder()
    {
        var orderId = Guid.NewGuid();
        var bookTitle = _bookTitles[_random.Next(_bookTitles.Length)];
        var price = Math.Round(_random.Next(999, 4999) / 100.0m, 2); 

        return new BookOrderSubmitted(orderId, bookTitle, price);
    }
}
