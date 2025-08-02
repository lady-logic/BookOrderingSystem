using BookOrderingSystem.Contracts;
using MassTransit;

namespace BookOrderingSystem.BillingService;

public class DeadLetterConsumer : IConsumer<Fault<BookOrderSubmitted>>
{
    public async Task Consume(ConsumeContext<Fault<BookOrderSubmitted>> context)
    {
        var fault = context.Message;
        var originalMessage = fault.Message;

        Console.WriteLine($"   DEAD LETTER: Order {originalMessage.OrderId} endgültig fehlgeschlagen!");
        Console.WriteLine($"   Buch: {originalMessage.BookTitle}");
        Console.WriteLine($"   Preis: {originalMessage.Price:C}");
        Console.WriteLine($"   Grund: {fault.Exceptions.FirstOrDefault()?.ExceptionType}");
        Console.WriteLine($"   Admin-Benachrichtigung verschickt!");
    }
}