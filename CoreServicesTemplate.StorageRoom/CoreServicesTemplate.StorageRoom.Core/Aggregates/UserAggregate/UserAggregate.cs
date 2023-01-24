using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Bases;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregate
{
    public class UserAggregate : AggEntityBase, IUserAggregateRoot
    {
        private readonly IConsolidator<AddressAggModel, AddressItem> _addressConsolidator;

        private string Name { get; set; }
        private string Surname { get; set; }
        private DateTime Birth { get; set; }

        private IAddressItem _addressItem;

        public UserAggregate(
            IConsolidator<AddressAggModel, AddressItem> addressConsolidator,
            IAddressItem addressItem)
        {
            _addressConsolidator = addressConsolidator;
            _addressItem = addressItem;
        }

        public Task<UserAggModel> CreateUser(UserAggModel userAggModel)
        {
            Name = userAggModel.Name;
            Surname = userAggModel.Surname;
            Birth = userAggModel.Birth;

            // decoupling from external model
            _addressItem = _addressConsolidator.ToData(userAggModel.AddressAggModel).Resolve();

            // do something

            // coupling with external model
            userAggModel.AddressAggModel = _addressConsolidator.ToDataReverse((AddressItem)_addressItem).Resolve();

            return Task.FromResult(userAggModel);
        }

        public Task<string> UserToString()
        {
            var user = $"{Name} {Surname}, {Birth}, {_addressItem.AddressToString()}";

            return Task.FromResult(user);
        }

        public Task<string> AddressToString()
        {
            var address = $"{_addressItem.AddressToString()}";

            return Task.FromResult(address);
        }
    }
}