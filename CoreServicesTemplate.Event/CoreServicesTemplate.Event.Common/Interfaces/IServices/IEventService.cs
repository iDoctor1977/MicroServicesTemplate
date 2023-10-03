﻿using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;

namespace CoreServicesTemplate.Event.Common.Interfaces.IServices
{
    public interface IEventService
    {
        Task<OperationResult<WalletApiDto>> GetWalletAsync(Guid ownerGuid);
    }
}