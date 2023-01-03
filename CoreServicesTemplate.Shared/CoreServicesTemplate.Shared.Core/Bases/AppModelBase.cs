using System;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public class AppModelBase : IAppModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
    }
}