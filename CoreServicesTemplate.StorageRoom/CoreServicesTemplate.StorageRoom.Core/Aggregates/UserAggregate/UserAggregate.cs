using AutoMapper;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Bases;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregate
{
    public class UserAggregate : AggregateBase, IUserAggregate
    {
        private readonly IConsolidator<AddressAggModel, AddressItem> _addressConsolidator;

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime Birth { get; private set; }

        public AddressItem AddressItem { get; private set; }

        public UserAggregate(
            IMapper mapper, 
            IConsolidator<AddressAggModel, AddressItem> addressConsolidator, 
            UserAggModel aggModel)
        {
            _addressConsolidator = addressConsolidator;

            mapper.Map(aggModel, this);
            AddressItem = _addressConsolidator.ToData(aggModel.AddressAggModel).Resolve();
        }

        public UserAggModel CreateUser(UserAggModel userAggModel)
        {
            // decoupling from external aggModel
            Guid = userAggModel.Guid;
            Name = userAggModel.Name;
            Surname = userAggModel.Surname;
            Birth = userAggModel.Birth;
            AddressItem = _addressConsolidator.ToData(userAggModel.AddressAggModel).Resolve();

            // do something

            // coupling with external aggModel
            userAggModel.AddressAggModel = _addressConsolidator.ToDataReverse(AddressItem).Resolve();

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