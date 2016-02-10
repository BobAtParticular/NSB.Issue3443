using System;
using NServiceBus;

namespace Issue3443
{
    class DoSomethingHandler : IHandleMessages<DoSomething>
    {
        public void Handle(DoSomething message)
        {
            Console.WriteLine("received message");
            throw new Exception("Something Bad Happened");
        }
    }
}