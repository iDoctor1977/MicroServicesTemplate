using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.StorageRoom.Common.Models
{
    public class UsersAppModel : IAppModel
    {
        public IEnumerable<UserAppModel> UsersModelList { get; set; }
    }
}