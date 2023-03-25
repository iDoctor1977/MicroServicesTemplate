using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Dashboard.Web.Models.Wallets;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Dashboard.Web.CustomMappers.Wallets
{
    public class WalletWebCustomMapper : WalletWebCustomMapperBase<WalletViewModel, WalletAppModel>
    {
        public WalletWebCustomMapper(IDefaultMapper<WalletViewModel, WalletAppModel> walletMapper) : base(walletMapper) { }

        public override WalletAppModel Map(WalletViewModel viewModel)
        {
            var appModel = base.Map(viewModel);

            return appModel;
        }

        public override WalletViewModel Map(WalletAppModel appModel)
        {
            var viewModel = base.Map(appModel);

            return viewModel;
        }
    }
}