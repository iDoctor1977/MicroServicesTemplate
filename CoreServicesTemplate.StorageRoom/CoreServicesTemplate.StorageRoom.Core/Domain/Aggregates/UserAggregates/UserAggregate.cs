using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;
using CoreServicesTemplate.StorageRoom.Core.Domain.SeedWork;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates.UserAggregates
{
    public class UserAggregate : IAggregate
    {
        private readonly IDefaultMapper<UserAggModel, UserAggregate> _userMapper;

        public Guid Guid { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime Birth { get; private set; }

        public AddressAggregate AddressItem { get; private set; }

        public UserAggregate(
            IAggregateFactory aggregateFactory,
            IDefaultMapper<UserAggModel, UserAggregate> userMapper,
            UserAggModel aggModel)
        {
            _userMapper = userMapper;

            if (aggModel is { Name: { }, Surname: { }, AddressAggModel: { } } && aggModel.Birth != DateTime.MinValue)
            {
                _userMapper.Map(aggModel, this);

                AddressItem = aggregateFactory.GenerateAggregate<AddressAggModel, AddressAggregate>(aggModel.AddressAggModel);

                if (Guid == Guid.Empty)
                {
                    Guid = Guid.NewGuid();
                }
            }
            else
            {
                throw new DomainValidationException<UserAggregate>("Guid is not valid");
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