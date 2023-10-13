using System.Globalization;
using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Dashboard.Web.Models.Wallets;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Dashboard.Web.CustomMappers.Wallets
{
    public class WalletWebCustomMapperBase<T1, T2> : CustomMapperBase<T1, T2> where  T1 : WalletViewBaseModel where T2 : WalletAppModelBase
    {
        protected WalletWebCustomMapperBase(IDefaultMapper<T1, T2> walletMapper) : base(walletMapper) { }

        public override T2 Map(T1 viewModel)
        {
            var appModel = base.Map(viewModel);
            appModel.Balance = decimal.Parse(viewModel.Balance);
            appModel.OperationAllowedBalance = decimal.Parse(viewModel.OperationAllowedBalance);
            appModel.TradingAllowedBalance = decimal.Parse(viewModel.TradingAllowedBalance);

            return appModel;
        }

        public override T1 Map(T2 appModel)
        {
            var viewModel = base.Map(appModel);
            viewModel.Balance = appModel.Balance.ToString(CultureInfo.InvariantCulture);
            viewModel.OperationAllowedBalance = appModel.OperationAllowedBalance.ToString(CultureInfo.InvariantCulture);
            viewModel.TradingAllowedBalance = appModel.TradingAllowedBalance.ToString(CultureInfo.InvariantCulture);

            return viewModel;
        }
    }
}