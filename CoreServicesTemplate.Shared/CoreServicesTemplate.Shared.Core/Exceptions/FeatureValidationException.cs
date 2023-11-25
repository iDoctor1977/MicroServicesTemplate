using System;

namespace CoreServicesTemplate.Shared.Core.Exceptions
{
    public class FeatureValidationException<T> : Exception where T : class
    {
        public string ClassName { get; set; } = nameof(T);

        public FeatureValidationException() { }
        public FeatureValidationException(string message) : base(message) { }
        public FeatureValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
