using System;
using NServiceBus;

namespace Issue3443
{
    class Program
    {
        static void Main(string[] args)
        {
            Configure.Serialization.Json();
            Configure configure = Configure.With();
            configure.DefineEndpointName("TestCustomPolicy");
            configure.DefaultBuilder();
            configure.UseInMemoryTimeoutPersister();
            configure.InMemorySubscriptionStorage();
            configure.UseTransport<Msmq>();
            var rp = new NServiceBusSecondLevelHandling();
            Configure.Features.SecondLevelRetries(s => s.CustomRetryPolicy(rp.RetryPolicy));

            using (IStartableBus startableBus = configure.UnicastBus().CreateBus())
            {
                IBus bus = startableBus.Start(() => configure.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());;
                while (Console.ReadLine() != null)
                {
                    bus.SendLocal(new DoSomething());
                }
            }
        }
    }
}
