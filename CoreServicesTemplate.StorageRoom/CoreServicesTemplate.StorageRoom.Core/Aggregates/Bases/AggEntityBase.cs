using System;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.Bases
{
    public class AggEntityBase
    {
        private int Id { get; set; }
        private Guid Guid { get; set; }
    }
}
