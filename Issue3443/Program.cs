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
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("TestCustomPolicy");
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            busConfiguration.SecondLevelRetries().CustomRetryPolicy(new NServiceBusSecondLevelHandling().RetryPolicy);

            using (IBus bus = Bus.Create(busConfiguration).Start())
            {
                while (Console.ReadLine() != null)
                {
                    bus.SendLocal(new DoSomething());
                }
            }
        }
    }
}
