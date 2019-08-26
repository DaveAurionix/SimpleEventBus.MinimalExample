using SimpleEventBus.AzureServiceBusTransport;

namespace SimpleEventBus.MinimalExample.Publisher.Configuration
{
    class Settings
    {
        public AzureServiceBusTransportSettings AzureServiceBusTransport { get; set; }
    }
}
