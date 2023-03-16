using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Address;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates
{
    public class AddressAggregate : IAggregate
    {
        private readonly IDefaultMapper<AddressAggModel, AddressAggregate> _addressMapper;
        private readonly ILogger<AddressAggregate> _logger;

        public Guid GuId { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }

        private AddressAggregate(
            IDefaultMapper<AddressAggModel, AddressAggregate> addressMapper,
            ILogger<AddressAggregate> logger)
        {
            _addressMapper = addressMapper;
            _logger = logger;
        }

        // Used to create new instance
        public AddressAggregate(
            IDefaultMapper<CreateAddressAggModel, AddressAggregate> createAddressMapper,
            IDefaultMapper<AddressAggModel, AddressAggregate> addressMapper,
            CreateAddressAggModel aggModel,
            ILogger<AddressAggregate> logger) : this(addressMapper, logger)
        {
            SharedConstruction(aggModel);

            createAddressMapper.Map(aggModel, this);

            GuId = Guid.NewGuid();
        }

        // Used for all other operations
        public AddressAggregate(
            IDefaultMapper<AddressAggModel, AddressAggregate> addressMapper,
            AddressAggModel aggModel, 
            ILogger<AddressAggregate> logger) : this(addressMapper, logger)
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
