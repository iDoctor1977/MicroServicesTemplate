using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;
using CoreServicesTemplate.StorageRoom.Core.Domain.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates.UserAggregates
{
    public class AddressAggregate : IAggregate
    {
        private readonly IDefaultMapper<AddressAggModel, AddressAggregate> _mapper;

        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }

        public AddressAggregate(IDefaultMapper<AddressAggModel, AddressAggregate> mapper, AddressAggModel appModel)
        {
            if (appModel is { Address1: { }, City: { }, State: { }, PostalCode: { } })
            {
                _mapper = mapper;

                _mapper.Map(appModel, this);
            }
            else
            {
                throw new DomainValidationException<AddressAggregate>("Guid is not valid");
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
