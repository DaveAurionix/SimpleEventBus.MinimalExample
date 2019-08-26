using SimpleEventBus.AzureServiceBusTransport;

namespace SimpleEventBus.MinimalExample.Subscriber.Configuration
{
    class Settings
    {
        public AzureServiceBusTransportSettings AzureServiceBusTransport { get; set; }
    }
}
