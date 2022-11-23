using MassTransit;
using MassTransitMQ.Infra.Data.Context;
using MassTransitMQ.Infra.Data.Repositories;
using MassTransitMQ.Infra.Data.Repositories.Interfaces;
using MassTransitMQ.Infra.Data.UoW;
using MassTransitMQ.Infra.Data.UoW.Interfaces;
using MassTransitMQ.Services.Background.Messaging;
using MassTransitMQ.Services.Background.Workers.Messaging.Consumer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System.Reflection;
using MassTransitMQ.Application.Queries.User.GetAllUsersQuery;

namespace MassTransitMQ.Infra.IoC;

public static class DependencyContainer
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, Assembly program){
        services.AddDbContext<ApplicationDbContext>(opts => 
            opts.UseMySql(
                configuration.GetConnectionString("DefaultConnection"), 
                new MySqlServerVersion(new Version(8, 0, 31)), 
                opts => opts.CommandTimeout(300)
            )
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        var assemblies = program.GetReferencedAssemblies();

        foreach (var assemblyName in assemblies)
        {
            Console.WriteLine($"[ASSEMBLY] {assemblyName.Name}");
        }

        services.AddMediatR(typeof(GetAllUsersQuery).GetTypeInfo().Assembly);

        services.AddHealthChecks();

        services.Configure<HealthCheckPublisherOptions>(options =>
        {
            options.Delay = TimeSpan.FromSeconds(2);
            options.Predicate = (check) => check.Tags.Contains("ready");
        });

        services.AddMassTransit(x =>    
        {
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
