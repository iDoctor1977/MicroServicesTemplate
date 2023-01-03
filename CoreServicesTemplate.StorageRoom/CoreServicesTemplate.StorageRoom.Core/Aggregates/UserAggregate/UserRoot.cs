using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregate
{
    public class UserRoot : IUserRoot
    {
        private readonly IConsolidator<UserAggModel, IUserItem> _userItemConsolidator;
        private readonly IConsolidator<UserAggModel, IAddressItem> _addressItemConsolidator;
        private IUserItem _userItem;
        private IAddressItem _addressItem;

        public UserRoot(
            IConsolidator<UserAggModel, IUserItem> userItemConsolidator, 
            IConsolidator<UserAggModel, IAddressItem> addressItemConsolidator, 
            IUserItem userItem, 
            IAddressItem addressItem)
        {
            _userItemConsolidator = userItemConsolidator;
            _addressItemConsolidator = addressItemConsolidator;
            _userItem = userItem;
            _addressItem = addressItem;
        }

        public Task<UserAggModel> CreateUser(UserAggModel userValueObject)
        {
            _userItem = _userItemConsolidator.ToData(userValueObject).Resolve();
            _addressItem = _addressItemConsolidator.ToData(userValueObject).Resolve();

            // do something

            var aggModel = _userItemConsolidator.ToDataReverse(_userItem).Resolve();
            aggModel = _addressItemConsolidator.ToDataReverse(_addressItem).Resolve();

            return Task.FromResult(aggModel);
        }

        public Task<string> UserToString()
        {
            var user = $"{_userItem.UserToString()}, {_addressItem.AddressToString()}";

            return Task.FromResult(user);
        }

        public Task<string> AddressToString()
        {
            return Task.FromResult(_addressItem.AddressToString());
        }
    }
}