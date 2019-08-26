using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleEventBus.AzureServiceBusTransport;
using System;

namespace SimpleEventBus.MinimalExample.Subscriber.Configuration
{
    static class Dependencies
    {
        public static IServiceProvider Install()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(@"Configuration/appsettings.json", optional: false)
                .AddJsonFile(@"Configuration/appsettings.Development.json", optional: true);
            var settings = configurationBuilder.Build().Get<Settings>();

            var services = new ServiceCollection();

            return services
                .AddSingleton(settings)
                .AddSingleton<Program>()
                .AddConfiguredLogging()
                .AddSimpleEventBus(settings.AzureServiceBusTransport)
                .BuildServiceProvider();
        }

        private static IServiceCollection AddConfiguredLogging(this IServiceCollection services)
            => services.AddLogging(
                    builder => builder
                        .AddConsole());

        private static IServiceCollection AddSimpleEventBus(this IServiceCollection services, AzureServiceBusTransportSettings azureServiceBusTransportSettings)
            => services.AddSimpleEventBus(
                    options => options
                        .UseEndpointName("SimpleEventBus.MinimalExample.Subscriber")
                        .UseAzureServiceBus(azureServiceBusTransportSettings)
                        .UseSingletonHandlersIn(typeof(Program).Assembly));
    }
}
