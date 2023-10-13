using System;

namespace CoreServicesTemplate.Shared.Core.EventModels.Wallet;

public class WalletCreatedBusDto
{
    public Guid OwnerGuid { get; set; }
    public bool IsCreated { get; set; }
}