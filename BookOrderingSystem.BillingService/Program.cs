﻿using BookOrderingSystem.BillingService;
using MassTransit;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<BookOrderConsumer>(); 
            x.AddConsumer<DeadLetterConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(Environment.GetEnvironmentVariable("RabbitMQ__Host") ?? "localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.UseMessageRetry(r =>
                {
                    r.Handle<PaymentDeclinedException>();
                    r.Interval(5, TimeSpan.FromSeconds(2));                       
                });

                cfg.ConfigureEndpoints(context); // Automatisches Routing!
            });
        });
    })
    .Build();

await host.RunAsync();