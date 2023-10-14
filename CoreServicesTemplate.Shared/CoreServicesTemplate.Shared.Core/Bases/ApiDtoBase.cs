using System;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public class ApiDtoBase : IApiDto
    {
        public Guid Guid { get; set; }
    }
}