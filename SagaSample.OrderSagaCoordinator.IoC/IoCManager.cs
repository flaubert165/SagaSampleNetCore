using MassTransit;
using MassTransit.RedisIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SagaSample.OrderSagaCoordinator.Infrastructure.Events.Clients.RabbitMQ.Events.Transactional;
using StackExchange.Redis;
using System;
using System.Reflection;

namespace SagaSample.OrderSagaCoordinator.IoC
{
    public static class IoCManager
    {
        public static void Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.GetExecutingAssembly());
                x.AddActivities(Assembly.GetExecutingAssembly());

                x.SetKebabCaseEndpointNameFormatter();

                x.AddSagaStateMachine<OrderStateMachine, OrderState>()
                    .RedisRepository(r =>
                    {
                        var configurationOptions = ConfigurationOptions.Parse(configuration["RedisConfiguration:PartialConnectionString"]);
                        configurationOptions.Password = configuration["RedisConfiguration:Password"];
                        configurationOptions.KeepAlive = 10;
                        configurationOptions.SyncTimeout = 500;
                        configurationOptions.AllowAdmin = true;
                        configurationOptions.ReconnectRetryPolicy = new LinearRetry(3000);

                        r.DatabaseConfiguration(configurationOptions);
                        r.ConcurrencyMode = ConcurrencyMode.Pessimistic;
                        r.KeyPrefix = "dev";
                        r.LockSuffix = "-lockage";
                        r.LockTimeout = TimeSpan.FromSeconds(90);
                    });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);

                    cfg.Host(configuration["RabbitMqConfig:Host"], configuration["RabbitMqConfig:VirtualHost"], h =>
                    {
                        h.Username(configuration["RabbitMqConfig:User"]);
                        h.Password(configuration["RabbitMqConfig:Password"]);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}
