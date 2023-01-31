namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregates
{
    public class AddressItem
    {
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }

        public string AddressToString()
        {
            string address = $"{Address1} {Address2}, {PostalCode}, {City}, {State}";

            return address;
        }
    }
}
