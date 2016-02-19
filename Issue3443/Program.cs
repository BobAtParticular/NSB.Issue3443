using System;
using NServiceBus;
using NServiceBus.Logging;

namespace Issue3443
{
    class Program
    {
        static void Main(string[] args)
        {
            LogManager.Use<DefaultFactory>().Level(LogLevel.Info);
            var configuration = new EndpointConfiguration();
            configuration.EndpointName("TestCustomPolicy");
            configuration.UseSerialization<JsonSerializer>();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.SecondLevelRetries().CustomRetryPolicy(new NServiceBusSecondLevelHandling().RetryPolicy);

            var bus = Endpoint.Start(configuration).Result;

            while (Console.ReadLine() != null)
            {
                bus.SendLocal(new DoSomething());
            }

            bus.Stop().RunSynchronously();
        }
    }
}
