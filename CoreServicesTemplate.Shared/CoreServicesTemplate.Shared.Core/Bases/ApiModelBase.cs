using System;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public class ApiModelBase : IApiModel
    {
        public Guid Guid { get; set; }
    }
}