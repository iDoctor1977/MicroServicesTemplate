using System.Globalization;
using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Dashboard.Web.Models.Wallets;
using CoreServicesTemplate.Shared.Core.Extensions;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Dashboard.Web.CustomMappers.Wallets
{
    public class CreateWalletWebCustomMapper : WalletWebCustomMapperBase<CreateWalletViewModel, CreateWalletAppModel>
    {
        public CreateWalletWebCustomMapper(IDefaultMapper<CreateWalletViewModel, CreateWalletAppModel> walletMapper) : base(walletMapper) { }

        public override CreateWalletAppModel Map(CreateWalletViewModel viewModel)
        {
            var appModel = base.Map(viewModel);
            appModel.DayTime = DateTime.ParseExact(viewModel.DayTime, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            return appModel;
        }

        public override CreateWalletViewModel Map(CreateWalletAppModel appModel)
        {
            var viewModel = base.Map(appModel);
            viewModel.DayTime = appModel.DayTime.ToStandardString();

            return viewModel;
        }
    }
}