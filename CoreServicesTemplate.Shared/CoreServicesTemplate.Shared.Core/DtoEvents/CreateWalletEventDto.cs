using System;

namespace CoreServicesTemplate.Shared.Core.DtoEvents;

public class CreateWalletEventDto
{
    public Guid OwnerGuid { get; set; }
    public bool IsCreated { get; set; }
}