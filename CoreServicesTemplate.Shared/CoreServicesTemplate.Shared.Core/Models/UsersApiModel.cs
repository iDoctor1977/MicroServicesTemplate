using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.Shared.Core.Models
{
    public class UsersApiModel : AModelBase
    {
        public IEnumerable<UserApiModel> UsersApiModelList { get; set; }
    }
}