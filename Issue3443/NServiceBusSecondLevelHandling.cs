using System;
using NServiceBus;

namespace Issue3443
{
    public class NServiceBusSecondLevelHandling
    {
        public TimeSpan RetryPolicy(TransportMessage transportMessage)
        {
            string value;

            int retries = transportMessage.Headers.TryGetValue(Headers.Retries, out value) ? int.Parse(value) : 0;

            Console.WriteLine("Inside custom retry policy: " + retries);

            if (retries > 4)
            {
                return TimeSpan.MinValue;
            }

            return TimeSpan.FromSeconds(2);
        }
    }
}