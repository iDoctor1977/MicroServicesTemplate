using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.Console.Common.Models
{
    public class UsersModel : AAppModelBase
    {
        public IEnumerable<UserModel> UsersModelList { get; set; }
    }
}