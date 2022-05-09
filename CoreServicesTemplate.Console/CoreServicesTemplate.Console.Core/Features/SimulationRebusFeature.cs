using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Shared.Core.QueueMessages;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Bus;

namespace CoreServicesTemplate.Console.Core.Features
{
    public class SimulationRebusFeature : ISimulationRebusFeature
    {
        private readonly IBus _bus;

        public SimulationRebusFeature(IServiceProvider service)
        {
            _bus = service.GetRequiredService<IBus>();
        }

        public async Task HandleAsync(SimulationMessage message)
        {
            try
            {
                await _bus.Send(message);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }
        }
    }
}
