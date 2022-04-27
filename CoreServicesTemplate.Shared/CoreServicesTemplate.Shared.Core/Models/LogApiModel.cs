using System;

namespace CoreServicesTemplate.Shared.Core.Models
{
    public class LogApiModel
    {
        public DateTime? LogTime { get; set; }
        public string Body { get; set; }
        public string Request { get; set; }
        public string IpAddress { get; set; }
    }
}
