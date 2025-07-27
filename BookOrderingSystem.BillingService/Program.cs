using BookOrderingSystem.BillingService;
using MassTransit;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<BookOrderConsumer>(); // Consumer registrieren!

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(Environment.GetEnvironmentVariable("RabbitMQ__Host") ?? "localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context); // Automatisches Routing!
            });
        });
    })
    .Build();

await host.RunAsync();