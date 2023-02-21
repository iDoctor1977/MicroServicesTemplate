namespace CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;

/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class UserDomainException : Exception
{
    public UserDomainException() { }

    public UserDomainException(string message) : base(message) { }

    public UserDomainException(string message, Exception innerException) : base(message, innerException) { }
}
