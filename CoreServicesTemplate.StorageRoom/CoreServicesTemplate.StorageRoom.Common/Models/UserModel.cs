using System;
using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.StorageRoom.Common.Models
{
    public class UserModel : AAppModelBase
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
    }
}