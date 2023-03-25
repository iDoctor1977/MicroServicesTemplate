namespace CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions
{
    public class DomainValidationException<T> : Exception where T : class
    {
        public string ClassName { get; set; } = nameof(T);

        public DomainValidationException() { }
        public DomainValidationException(string message) : base(message) { }
        public DomainValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
