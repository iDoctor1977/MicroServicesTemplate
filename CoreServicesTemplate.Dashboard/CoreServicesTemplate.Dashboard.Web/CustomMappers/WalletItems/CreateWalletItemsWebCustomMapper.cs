using CoreServicesTemplate.Dashboard.Common.Models.AppModels.WalletItems;
using CoreServicesTemplate.Dashboard.Web.Models.WalletItems;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Dashboard.Web.CustomMappers.WalletItems
{
    public class CreateWalletItemsWebCustomMapper : WalletItemWebCustomMapperBase<CreateWalletItemViewModel, WalletItemAppModel>
    {
        public CreateWalletItemsWebCustomMapper(IDefaultMapper<CreateWalletItemViewModel, WalletItemAppModel> walletItemMapper) : base(walletItemMapper) { }

        public override WalletItemAppModel Map(CreateWalletItemViewModel viewModel)
        {
            var appModel = base.Map(viewModel);

            return appModel;
        }

        public override CreateWalletItemViewModel Map(WalletItemAppModel appModel)
        {
            var viewModel = base.Map(appModel);

            return viewModel;
        }

    }
}