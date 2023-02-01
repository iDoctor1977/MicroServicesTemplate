using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregates
{
    public class AddressItem : IAggregate
    {
        private readonly IMapperService<AddressAggModel, AddressItem> _mapper;

        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }

        public AddressItem(IMapperService<AddressAggModel, AddressItem> mapper, AddressAggModel appModel)
        {
            _mapper = mapper;

            _mapper.Map(appModel, this);
        }

        public string AddressToString()
        {
            string address = $"{Address1} {Address2}, {PostalCode}, {City}, {State}";

            return address;
        }
    }
}
