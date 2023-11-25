using CoreServicesTemplate.Dashboard.Common.Models.AppModels.Wallets;
using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

namespace CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;

public interface IGetWalletFeature : IQueryHandler<Guid, WalletAppModel> { }