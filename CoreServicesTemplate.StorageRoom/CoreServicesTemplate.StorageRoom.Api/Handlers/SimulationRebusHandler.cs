using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.QueueMessages;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Bus;
using Rebus.Handlers;

namespace CoreServicesTemplate.StorageRoom.Api.Handlers
{
    public class SimulationRebusHandler : IHandleMessages<SimulationMessage>
    {
        private readonly IBus _bus;

        public SimulationRebusHandler(IServiceProvider service)
        {
            _bus = service.GetRequiredService<IBus>();
        }

        public async Task Handle(SimulationMessage message)
        {
            Console.WriteLine("SimulationRebusHandler have received message");
            Console.WriteLine(message.Content);

            await Task.CompletedTask;
        }
    }
}