using System;
using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IEvents;

public interface IEventBus<in TIn> : IDisposable where TIn : class
{
    Task PublishAsync(TIn payload);
}