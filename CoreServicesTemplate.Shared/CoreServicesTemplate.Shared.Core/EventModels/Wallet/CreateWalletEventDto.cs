using System;

namespace CoreServicesTemplate.Shared.Core.EventModels.Wallet;

public class CreateWalletEventDto
{
    public Guid OwnerGuid { get; set; }
    public bool IsCreated { get; set; }
}