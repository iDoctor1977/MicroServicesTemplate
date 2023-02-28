using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.StorageRoom.Common.AppModels;

public class UserAppModel : AppModelBase
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birth { get; set; }

    public AddressAppModel AddressAppModel { get; set; }
}