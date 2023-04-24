using System;
using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IEvents;

public interface IEventHandler : IDisposable
{
    Task ExecuteAsync();
}