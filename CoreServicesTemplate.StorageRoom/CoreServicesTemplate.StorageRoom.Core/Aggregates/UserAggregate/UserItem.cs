using System;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.SeedWork;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregate
{
    public class UserItem : DomainEntityBase, IUserItem
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }

        public string UserToString()
        {
            return $"{Name} {Surname}, {Birth}";
        }
    }
}
