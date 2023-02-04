using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Bases;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.SeedWork;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregates
{
    public class UserAggregate : AggregateBase, IAggregate
    {
        private readonly IAggregateFactory _aggregateFactory;
        private readonly IDefaultMapper<UserAggModel,UserAggregate>_userMapper;
        private readonly IDefaultMapper<AddressAggModel, AddressItem> _addressMapper;

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime Birth { get; private set; }

        public AddressItem AddressItem { get; private set; }

        public UserAggregate(
            IAggregateFactory aggregateFactory,
            IDefaultMapper<UserAggModel, UserAggregate> userMapper,
            IDefaultMapper<AddressAggModel, AddressItem> addressMapper,
            UserAggModel aggModel)
        {
            _aggregateFactory = aggregateFactory;
            _userMapper = userMapper;
            _addressMapper = addressMapper;

            _userMapper.Map(aggModel, this);
            AddressItem = _aggregateFactory.GenerateAggregate<AddressAggModel, AddressItem>(aggModel.AddressAggModel);
        }

        public UserAggModel CreateUser(UserAggModel userAggModel)
        {
            // decoupling from external aggModel
            _userMapper.Map(userAggModel, this);
            AddressItem = _addressMapper.Map(userAggModel.AddressAggModel, AddressItem); // AddressItem same to this

            // do something
            Guid = Guid.NewGuid();

            // coupling with external aggModel
            userAggModel = _userMapper.Map(this);
            userAggModel.AddressAggModel = _addressMapper.Map(AddressItem);

            return userAggModel;
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
    }
}