using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;

public interface IGetTradingAvailableBalanceFeature : IQueryHandler<Guid, decimal> { }