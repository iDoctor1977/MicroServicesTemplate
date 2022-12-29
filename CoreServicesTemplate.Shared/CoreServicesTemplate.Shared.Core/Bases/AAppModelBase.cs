using System;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public abstract class AAppModelBase : IAppModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
    }
}