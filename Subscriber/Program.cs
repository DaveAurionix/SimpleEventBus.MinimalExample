using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleEventBus.MinimalExample.Subscriber.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleEventBus.MinimalExample.Subscriber
{
    class Program
    {
        private readonly Endpoint endpoint;
        private readonly ILogger<Program> logger;

        /// <summary>
        /// Creates a new instance of the Program class.
        /// </summary>
        /// <param name="endpoint">The SimpleEventBus messaging endpoint that this program hosts.</param>
        /// <param name="logger">An instance of a Microsoft Logger that can be used to log messages (for example, to the console)</param>
        public Program(Endpoint endpoint, ILogger<Program> logger)
        {
            this.endpoint = endpoint;
            this.logger = logger;
        }

        /// <summary>
        /// This is the entry-point for the program.  .NET will call this method first.
        /// </summary>
        /// <returns>A Task that is awaited until it finishes (when the program exits)</returns>
        static Task Main()
            => Dependencies
                .Install()
                .GetRequiredService<Program>()
                .Run();

        /// <summary>
        /// We call this Run method from the Main method. The Main method is a static method meaning we don't have access to constructor-injected dependencies
        /// such as Endpoint and ILogger<>. This Run method is called on an instance of Program that has been created for us by the Microsoft Dependency Injection framework.
        /// When created, it was given instances of dependencies such as Endpoint and ILogger<>.
        /// </summary>
        /// <returns>A Task that can be awaited until this Run method has finished its work.</returns>
        async Task Run()
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                AppDomain.CurrentDomain.ProcessExit += (sender, arguments)
                    =>
                    {
                        logger.LogWarning("Subscriber is stopping.");
                        cancellationTokenSource.Cancel();
                    };

                logger.LogInformation("Subscriber is listening - close the window to stop it");

                await endpoint.StartWaitThenShutDown(
                    cancellationTokenSource.Token);
            }
        }
    }
}
