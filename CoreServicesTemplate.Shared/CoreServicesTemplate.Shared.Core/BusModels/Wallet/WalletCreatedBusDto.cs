using System;

namespace CoreServicesTemplate.Shared.Core.BusModels.Wallet;

public class WalletCreatedBusDto
{
    public Guid OwnerGuid { get; set; }
    public bool IsCreated { get; set; }
}