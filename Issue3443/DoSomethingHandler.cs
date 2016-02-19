using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Issue3443
{
    class DoSomethingHandler : IHandleMessages<DoSomething>
    {

        public Task Handle(DoSomething message, IMessageHandlerContext context)
        {
            Console.WriteLine("received message");
            throw new Exception("Something Bad Happened");
        }
    }
}