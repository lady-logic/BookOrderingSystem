using BookOrderingSystem.Contracts;
using MassTransit;

namespace BookOrderingSystem.BillingService;

public class BookOrderConsumer : IConsumer<BookOrderSubmitted>
{
    public async Task Consume(ConsumeContext<BookOrderSubmitted> context)
    {
        var order = context.Message;

        // 1. Start-Log
        Console.WriteLine($"Payment wird verarbeitet für Order {order.OrderId} - {order.BookTitle} ({order.Price:C})");

        // 2. Payment-Simulation
        await Task.Delay(2000); // 2 Sekunden "Processing"

        // 3. PaymentProcessed senden
        await context.Publish(new PaymentProcessed(order.OrderId, true));

        // 4. Success-Log  
        Console.WriteLine($"Payment erfolgreich für Order {order.OrderId}");
    }
}
