using System;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Shared.Core.Dtos
{
    public class LogApiDto : IAppModel
    {
        public DateTime? LogTime { get; set; }
        public string Body { get; set; }
        public string Request { get; set; }
        public string IpAddress { get; set; }
    }
}
