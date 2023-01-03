using System;
using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.StorageRoom.Common.Models
{
    public class UserAppModel : AppModelBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}