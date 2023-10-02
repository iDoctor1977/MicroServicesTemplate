﻿using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models.Wallet;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures
{
    public interface ICreateWalletFeature : ICommandHandler<CreateWalletAppDto> { }
}