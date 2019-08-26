using Microsoft.Extensions.DependencyInjection;
using SimpleEventBus.Abstractions.Outgoing;
using SimpleEventBus.MinimalExample.Contract;
using SimpleEventBus.MinimalExample.Publisher.Configuration;
using System;
using System.Threading.Tasks;

namespace SimpleEventBus.MinimalExample.Publisher
{
    class Program
    {
        private readonly Endpoint endpoint;
        private readonly IMessagePublisher messagePublisher;

        public Program(Endpoint endpoint, IMessagePublisher messagePublisher)
        {
            this.endpoint = endpoint;
            this.messagePublisher = messagePublisher;
        }

        static Task Main()
            => Dependencies
                .Install()
                .GetRequiredService<Program>()
                .Run();

        async Task Run()
        {
            await PublishEvents();

            await endpoint.ShutDown();
        }

        private async Task PublishEvents()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Press the A, B or C keys to publish a test event (then watch the Subscriber process output to see it being handled)");
                Console.WriteLine("Press any other key to exit");
                Console.WriteLine();

                var keyPressed = Console.ReadKey().Key;
                switch (keyPressed)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.B:
                    case ConsoleKey.C:
                        await messagePublisher
                            .PublishEvent(
                                new SomethingHappened
                                {
                                    KeyPressed = keyPressed.ToString()
                                });
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
