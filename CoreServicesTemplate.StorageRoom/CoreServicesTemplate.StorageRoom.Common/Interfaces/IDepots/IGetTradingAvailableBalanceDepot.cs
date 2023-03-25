using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Wallet;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;

public interface IGetTradingAvailableBalanceDepot : IQueryHandler<Guid, WalletModel> { }