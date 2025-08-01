﻿using BookOrderingSystem.Contracts;
using MassTransit;

namespace BookOrderingSystem.BillingService;

public class BookOrderConsumer : IConsumer<BookOrderSubmitted>
{
    private readonly Random _random = new();

    public async Task Consume(ConsumeContext<BookOrderSubmitted> context)
    {
        var order = context.Message;

        // 1. Start-Log
        Console.WriteLine($"Payment wird verarbeitet für Order {order.OrderId} - {order.BookTitle} ({order.Price:C})");

        if (_random.Next(100) < 30)
        {
            Console.WriteLine($"Payment wird abgelehnt für Order {order.OrderId}");
            var failureType = _random.Next(3); 

            switch (failureType)
            {
                case 0:
                    Console.WriteLine($"Payment abgelehnt - Kreditkarte für Order {order.OrderId}");
                    throw new PaymentDeclinedException($"Kreditkarte für Order {order.OrderId} wurde abgelehnt");

                case 1:
                    Console.WriteLine($"Payment abgelehnt - Nicht genügend Guthaben für Order {order.OrderId}");
                    throw new InsufficientFundsException($"Nicht genügend Guthaben für Order {order.OrderId}");

                case 2:
                    Console.WriteLine($"Payment abgelehnt - Ungültige Kreditkarte für Order {order.OrderId}");
                    throw new InvalidCreditCardException($"Ungültige Kreditkarte für Order {order.OrderId}");
            }
        }

        // 2. Payment-Simulation
        await Task.Delay(2000); // 2 Sekunden "Processing"

        // 3. PaymentProcessed senden
        await context.Publish(new PaymentProcessed(order.OrderId, true));

        // 4. Success-Log  
        Console.WriteLine($"Payment erfolgreich für Order {order.OrderId}");
    }
}
