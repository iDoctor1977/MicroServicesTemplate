using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.Wallets;
using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

namespace CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;

public interface IGetWalletService : IQueryHandler<Guid, WalletModel> { }