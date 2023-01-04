using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregate
{
    public class AddressItem : IAddressItem
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public Task<string> AddressToString()
        {
            string address = $"{Address1} {Address2}, {PostalCode}, {City}, {State}";

            return Task.FromResult(address);
        }
    }
}
