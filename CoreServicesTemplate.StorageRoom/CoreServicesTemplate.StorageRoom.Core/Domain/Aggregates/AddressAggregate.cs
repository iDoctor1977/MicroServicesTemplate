using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Address;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates
{
    public class AddressAggregate : IAggregate
    {
        private readonly IDefaultMapper<CreateAddressAggModel, AddressAggregate> _baseAddressMapper;
        private readonly IDefaultMapper<AddressAggModel, AddressAggregate> _addressMapper;

        public Guid GuId { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }

        private AddressAggregate(
            IDefaultMapper<AddressAggModel, AddressAggregate> addressMapper,
            IDefaultMapper<CreateAddressAggModel, AddressAggregate> baseAddressMapper)
        {
            _baseAddressMapper = baseAddressMapper;
            _addressMapper = addressMapper;
        }

        // Used to create new instance
        public AddressAggregate(
            IDefaultMapper<AddressAggModel, AddressAggregate> addressMapper, 
            IDefaultMapper<CreateAddressAggModel, AddressAggregate> baseAddressMapper,
            CreateAddressAggModel aggModel) : this(addressMapper, baseAddressMapper)
        {
            SharedConstruction(aggModel);

            _baseAddressMapper.Map(aggModel, this);

            GuId = Guid.NewGuid();
        }

        // Used for all other operations
        public AddressAggregate(
            IDefaultMapper<AddressAggModel, AddressAggregate> addressMapper,
            IDefaultMapper<CreateAddressAggModel, AddressAggregate> baseAddressMapper,
            AddressAggModel aggModel) : this(addressMapper, baseAddressMapper)
        {
            if (aggModel.GuId.Equals(null) || aggModel.GuId == Guid.Empty)
            {
                throw new DomainValidationException<AddressAggregate>("Guid is not valid");
            }
            if (aggModel.Address2.Equals(null))
            {
                throw new DomainValidationException<AddressAggregate>("Address2 is not valid");
            }

            SharedConstruction(aggModel);
        }

        private void SharedConstruction(AddressAggModelBase aggModel)
        {
            if (aggModel.Address1.Equals(null))
            {
                throw new DomainValidationException<AddressAggregate>("Address1 is not valid");
            }
            if (aggModel.City.Equals(null))
            {
                throw new DomainValidationException<AddressAggregate>("City is not valid");
            }
            if (aggModel.PostalCode.Equals(null))
            {
                throw new DomainValidationException<AddressAggregate>("Postal code is not valid");
            }
            if (aggModel.State.Equals(null))
            {
                throw new DomainValidationException<AddressAggregate>("State is not valid");
            }
        }

        public string AddressToString()
        {
            string address = $"{Address1}, {Address2}, {PostalCode}, {City}, {State}";

            return address;
        }

        public AddressAggModel ToModel()
        {
            var toModel = _addressMapper.Map(this);

            return toModel;
        }
    }
}
