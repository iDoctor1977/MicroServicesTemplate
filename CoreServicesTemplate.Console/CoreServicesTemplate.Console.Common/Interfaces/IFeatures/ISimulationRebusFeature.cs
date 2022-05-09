using CoreServicesTemplate.Shared.Core.Interfaces.ICqrs;
using CoreServicesTemplate.Shared.Core.QueueMessages;

namespace CoreServicesTemplate.Console.Common.Interfaces.IFeatures
{
    public interface ISimulationRebusFeature : ICqrsCommandHandler<SimulationMessage> { }
}