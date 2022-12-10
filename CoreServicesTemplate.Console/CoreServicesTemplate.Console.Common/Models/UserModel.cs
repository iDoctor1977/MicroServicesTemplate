using System;
using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.Console.Common.Models
{
    public class UserModel : AAppModelBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
    }
}