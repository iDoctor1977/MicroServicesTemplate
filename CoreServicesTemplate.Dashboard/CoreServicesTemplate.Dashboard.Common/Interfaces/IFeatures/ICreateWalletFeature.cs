using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

namespace CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;

public interface ICreateWalletFeature : ICommandHandler<CreateWalletAppModel> { }