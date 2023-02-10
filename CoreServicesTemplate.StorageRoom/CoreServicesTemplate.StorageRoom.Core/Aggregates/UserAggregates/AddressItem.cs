using System.ComponentModel.DataAnnotations;
using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregates
{
    public class AddressItem : IAggregate
    {
        private readonly IDefaultMapper<AddressAggModel, AddressItem> _mapper;

        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }

        public AddressItem(IDefaultMapper<AddressAggModel, AddressItem> mapper, AddressAggModel appModel)
        {
            if (appModel is { Address1: { }, City: { }, State: { }, PostalCode: { } })
            {
                _mapper = mapper;

                _mapper.Map(appModel, this);
            }
            else
            {
                throw new Exception("One or more properties in not set correctly.");
            }
        }

        public string AddressToString()
        {
            string address = $"{Address1}, {Address2}, {PostalCode}, {City}, {State}";

            return address;
        }

        public AddressAggModel ToModel()
        {
            var toModel = _mapper.Map(this);

            return toModel;
        }
    }
}
