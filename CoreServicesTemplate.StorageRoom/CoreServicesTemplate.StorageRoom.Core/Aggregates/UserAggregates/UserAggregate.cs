using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Bases;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregates
{
    public class UserAggregate : AggregateBase, IAggregate
    {
        private readonly IMapping<AddressAggModel, AddressItem> _addressMapper;

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime Birth { get; private set; }

        public AddressItem AddressItem { get; private set; }

        public UserAggregate(
            IMapping<AddressAggModel, AddressItem> addressMapper,
            IMapping<UserAggModel, UserAggregate> userMapper, 
            UserAggModel aggModel)
        {
            _addressMapper = addressMapper;

            userMapper.Map(aggModel, this);
            AddressItem = addressMapper.Map(aggModel.AddressAggModel);
        }

        public UserAggModel CreateUser(UserAggModel userAggModel)
        {
            // decoupling from external aggModel
            Guid = userAggModel.Guid;
            Name = userAggModel.Name;
            Surname = userAggModel.Surname;
            Birth = userAggModel.Birth;
            AddressItem = _addressMapper.Map(userAggModel.AddressAggModel);

            // do something

            // coupling with external aggModel
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