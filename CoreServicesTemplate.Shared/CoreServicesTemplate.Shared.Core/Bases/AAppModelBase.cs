using System;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public abstract class AAppModelBase
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
    }
}