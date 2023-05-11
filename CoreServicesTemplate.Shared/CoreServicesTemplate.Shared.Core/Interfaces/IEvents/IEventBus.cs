using System;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IEvents;

public interface IEventBus<in TIn> : IDisposable where TIn : class
{
    void Publish(TIn payload);
}