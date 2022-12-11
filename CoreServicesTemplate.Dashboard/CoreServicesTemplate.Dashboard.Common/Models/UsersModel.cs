using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.Dashboard.Common.Models
{
    public class UsersModel : AAppModelBase
    {
        public IEnumerable<UserModel> UsersModelList { get; set; }
    }
}