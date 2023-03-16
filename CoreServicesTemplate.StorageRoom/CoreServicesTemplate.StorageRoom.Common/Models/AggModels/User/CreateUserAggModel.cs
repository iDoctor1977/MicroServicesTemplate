using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Address;

namespace CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User
{
    /// <summary>
    /// This model is use just to create the new class.
    /// </summary>
    public class CreateUserAggModel : UserAggModelBase
    {
        public CreateAddressAggModel AddressAggModel { get; set; }
    }
}
