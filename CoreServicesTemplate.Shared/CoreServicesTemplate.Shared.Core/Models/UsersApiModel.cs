using System.Collections.Generic;

namespace CoreServicesTemplate.Shared.Core.Models
{
    public class UsersApiModel : ABaseModel
    {
        public IEnumerable<UserApiModel> UsersApiModelList { get; set; }
    }
}