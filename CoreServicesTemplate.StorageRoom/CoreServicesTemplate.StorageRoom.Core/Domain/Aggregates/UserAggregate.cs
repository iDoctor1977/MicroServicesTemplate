using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Address;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;
using CoreServicesTemplate.StorageRoom.Core.Domain.SeedWork;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates
{
    public class UserAggregate : IAggregate
    {
        private readonly IDefaultMapper<CreateUserAggModel, UserAggregate> _createUserMapper;
        private readonly IDefaultMapper<UserAggModel, UserAggregate> _userMapper;
        private readonly Logger<UserAggregate>_logger;

        public Guid Guid { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime Birth { get; private set; }

        public AddressAggregate AddressItem { get; private set; }

        private UserAggregate(
            IDefaultMapper<CreateUserAggModel, UserAggregate> createUserMapper, 
            IDefaultMapper<UserAggModel, UserAggregate> userMapper, 
            Logger<UserAggregate> logger)
        {
            _createUserMapper = createUserMapper;
            _userMapper = userMapper;
            _logger = logger;
        }

        // Used to create new instance
        public UserAggregate(IAggregateFactory aggregateFactory,
            IDefaultMapper<CreateUserAggModel, UserAggregate> baseUserMapper,
            IDefaultMapper<UserAggModel, UserAggregate> userMapper,
            CreateUserAggModel aggModel, 
            Logger<UserAggregate> logger) : this(baseUserMapper, userMapper, logger)
        {
            SharedConstruction(aggModel);

            try
            {
                AddressItem = aggregateFactory.GenerateAggregate<AddressAggModel, AddressAggregate>(aggModel.AddressAggModel);
            }
            catch (DomainValidationException<AddressAggregate> e)
            {
                _logger.LogCritical($"{GetType().Name}: {e.Message}");
                throw new DomainValidationException<UserAggregate>("Wallet item generation failed", e);
            }

            _createUserMapper.Map(aggModel, this);

            Guid = Guid.NewGuid();
        }

        // Used for existing instance
        public UserAggregate(
            IAggregateFactory aggregateFactory,
            IDefaultMapper<CreateUserAggModel, UserAggregate> baseUserMapper,
            IDefaultMapper<UserAggModel, UserAggregate> userMapper,
            UserAggModel aggModel, 
            Logger<UserAggregate> logger) : this(baseUserMapper, userMapper, logger)
        {
            if (aggModel.Guid.Equals(null) || aggModel.Guid == Guid.Empty)
            {
                throw new DomainValidationException<UserAggregate>("Guid is not valid");
            }
            if (aggModel.Birth.Equals(null) || aggModel.Birth == DateTime.MinValue)
            {
                throw new DomainValidationException<UserAggregate>("Birth is not valid");
            }

            SharedConstruction(aggModel);

            _userMapper.Map(aggModel, this);
        }

        private void SharedConstruction(UserAggModelBase aggModel)
        {
            if (aggModel.Name.Equals(null))
            {
                throw new DomainValidationException<UserAggregate>("Name is not valid");
            }
            if (aggModel.Surname.Equals(null))
            {
                throw new DomainValidationException<UserAggregate>("Surname is not valid");
            }
            if (aggModel.AddressAggModel.Equals(null))
            {
                throw new DomainValidationException<UserAggregate>("Address is not valid");
            }
        }

        public string UserToString()
        {
            var user = $"{Name} {Surname}, {Birth}, {AddressItem.AddressToString()}";

            return user;
        }

        public string AddressToString()
        {
            var address = $"{AddressItem.AddressToString()}";

            return address;
        }

        public UserAggModel ToModel()
        {
            var toModel = _userMapper.Map(this);
            toModel.AddressAggModel = AddressItem.ToModel();

            return toModel;
        }
    }
}