using System;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public abstract class AApiModelBase : IApiModel
    {
        public Guid Guid { get; set; }
    }
}