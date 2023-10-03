using System;

namespace CoreServicesTemplate.Shared.Core.Events;

public class CreateNewWalletEventDto
{
    public Guid OwnerGuid { get; set; }
    public bool IsCreated { get; set; }
}