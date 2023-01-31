using System;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Shared.Core.Models
{
    public class LogApiModel : IAppModel
    {
        public DateTime? LogTime { get; set; }
        public string Body { get; set; }
        public string Request { get; set; }
        public string IpAddress { get; set; }
    }
}
