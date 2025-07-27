using BookOrderingSystem.Contracts;
using MassTransit;

namespace BookOrderingSystem.NotificationService;

public class PaymentProcessedConsumer : IConsumer<PaymentProcessed>
{
    public async Task Consume(ConsumeContext<PaymentProcessed> context)
    {
        var payment = context.Message;
        Console.WriteLine($"Payment processed for Order {payment.OrderId}");
    }
}
