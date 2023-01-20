using System;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public class AppModelBase : IAppModel
    {
        public Guid Guid { get; set; }
    }
}