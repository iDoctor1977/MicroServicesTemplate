using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.AggModels;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates.UserAggregates
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
                throw new UserDomainException("One or more address properties in not valid.");
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
