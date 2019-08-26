using Microsoft.Extensions.Logging;
using SimpleEventBus.Abstractions.Incoming;
using SimpleEventBus.MinimalExample.Contract;
using System.Threading.Tasks;

namespace SimpleEventBus.MinimalExample.Subscriber.Handlers
{
    public class SomethingHappenedHandler : IHandles<SomethingHappened>
    {
        private readonly ILogger<SomethingHappenedHandler> logger;

        public SomethingHappenedHandler(ILogger<SomethingHappenedHandler> logger)
        {
            this.logger = logger;
        }
        public Task HandleMessage(SomethingHappened message)
        {
            logger.LogInformation(
                $"Key {message.KeyPressed} was pressed.");

            return Task.CompletedTask;
        }
    }
}
