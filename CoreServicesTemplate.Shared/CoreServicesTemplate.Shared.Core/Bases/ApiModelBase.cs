using System;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public class ApiModelBase : IApiModel
    {
        public Guid Guid { get; set; }
    }
}