using System;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.SeedWork
{
    public class DomainEntityBase
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
    }
}
