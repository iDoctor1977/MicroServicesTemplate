using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.Shared.Core.Models
{
    public class UsersApiModel : ApiModelBase
    {
        [Required]
        public IEnumerable<UserApiModel> UsersApiModelList { get; set; }
    }
}