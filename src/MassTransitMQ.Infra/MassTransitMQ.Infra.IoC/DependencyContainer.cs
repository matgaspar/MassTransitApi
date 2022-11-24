using MassTransit;
using MassTransitMQ.Application.Consumer;
using MassTransitMQ.Application.Services;
using MassTransitMQ.Domain.Interfaces.Data;
using MassTransitMQ.Domain.Interfaces.Data.Repositories;
using MassTransitMQ.Domain.Interfaces.Services;
using MassTransitMQ.Infra.Data.Context;
using MassTransitMQ.Infra.Data.Repositories;
using MassTransitMQ.Infra.Data.UoW;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System.Reflection;
using MassTransitMQ.Application.Queries.User.FindById;
using MassTransitMQ.Domain.Mapping;

namespace MassTransitMQ.Infra.IoC;

public static partial class DependencyContainer
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, Assembly assembly){

        #region Dbcontext
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
        services.AddScoped<IOrderRepository, OrderRepository>();
        #endregion
        
        #region MassTransit (RabbitMQ)
        services.AddMassTransit(x =>    
        {
            x.AddConsumer<OrderConsumer>();
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
                    e.ConfigureConsumer<OrderConsumer>(context);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        #endregion

        #region Health Endpoint
        services.AddHealthChecks();
        services.Configure<HealthCheckPublisherOptions>(options =>
        {
            options.Delay = TimeSpan.FromSeconds(2);
            options.Predicate = (check) => check.Tags.Contains("ready");
        });
        #endregion

        #region Injection Dependencies
        services.AddMediatR(typeof(FindUserById).Assembly);

        services.AddAutoMapper(cfg => {
            cfg.AddProfile<UserMappingProfile>();
            cfg.AddProfile<OrderMappingProfile>();
        });

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IOrderService, OrderService>();
        #endregion


        #region Workers

        //services.AddHostedService<ConsumerMessagingWorker>();

        #endregion
    }
}
