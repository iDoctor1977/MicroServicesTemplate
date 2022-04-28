using System;
using CoreServicesTemplate.Shared.Core.Interfaces.ICqrs;

namespace CoreServicesTemplate.Shared.Core.Models
{
    public abstract class ABaseModel : ICqrsQueryBase, ICqrsCommandBase
    {
        public Guid Guid { get; set; }
        public PagingData PagingData { get; set; }
    }
}