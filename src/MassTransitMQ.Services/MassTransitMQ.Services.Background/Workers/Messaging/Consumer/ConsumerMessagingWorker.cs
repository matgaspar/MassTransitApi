using Microsoft.Extensions.Hosting;

namespace MassTransitMQ.Services.Background.Workers.Messaging.Consumer
{
    public class ConsumerMessagingWorker : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken){
            return Task.CompletedTask;
        }
    }
}