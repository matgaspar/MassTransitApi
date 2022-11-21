using MassTransit;
using MassTransitMQ.Services.Background.Messaging;
using MassTransitMQ.Services.Background.Workers.Messaging.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MassTransitMQ.Infra.IoC;

public static class DependencyContainer
{
    public static void Register(IServiceCollection services){
        services.AddHealthChecks();

        services.Configure<HealthCheckPublisherOptions>(options =>
        {
            options.Delay = TimeSpan.FromSeconds(2);
            options.Predicate = (check) => check.Tags.Contains("ready");
        });

        services.AddMassTransit(x =>    
        {
            // x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg => 
            // {
            //     cfg.Host("localhost", "/", h => 
            //     {
            //         h.Username("guest");
            //         h.Password("guest");
            //     });
            // }));
            x.AddConsumer<OrderMessagingConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h => 
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                
                cfg.ReceiveEndpoint("OrderService", e =>
                {
                    e.PrefetchCount = 10;
                    e.UseMessageRetry(r => r.Interval(2, 100));
                    e.ConfigureConsumer<OrderMessagingConsumer>(context);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        
        services.AddHostedService<ConsumerMessagingWorker>();
    }    
}
