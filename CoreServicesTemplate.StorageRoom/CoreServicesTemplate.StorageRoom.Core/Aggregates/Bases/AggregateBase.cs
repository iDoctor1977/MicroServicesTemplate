namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.Bases
{
    public class AggregateBase
    {
        public int Id { get; protected set; }
        public Guid Guid { get; protected set; }
    }
}
