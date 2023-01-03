using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.StorageRoom.Common.Models
{
    public class UsersAppModel : IAppModel
    {
        public IEnumerable<UserAppModel> UsersModelList { get; set; }
    }
}