using CoreServicesTemplate.Dashboard.Common.Models.AppModels.WalletItems;
using CoreServicesTemplate.Dashboard.Web.Models.WalletItems;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using System.Globalization;

namespace CoreServicesTemplate.Dashboard.Web.CustomMappers.WalletItems
{
    public class WalletItemWebCustomMapperBase<T1, T2> : CustomMapperBase<T1, T2> where  T1 : WalletItemViewBaseModel where T2 : WalletItemAppModelBase
    {
        protected WalletItemWebCustomMapperBase(IDefaultMapper<T1, T2> walletMapper) : base(walletMapper) { }

        public override T2 Map(T1 viewModel)
        {
            var appModel = base.Map(viewModel);
            appModel.Amount = decimal.Parse(viewModel.Amount);
            appModel.BuyPrice = decimal.Parse(viewModel.BuyPrice);
            appModel.BuyDate = DateTime.ParseExact(viewModel.BuyDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            appModel.DateUpdated = DateTime.ParseExact(viewModel.DateUpdated, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            return appModel;
        }

        public override T1 Map(T2 appModel)
        {
            var viewModel = base.Map(appModel);
            viewModel.Amount = appModel.Amount.ToString(CultureInfo.InvariantCulture);
            viewModel.BuyPrice = appModel.BuyPrice.ToString(CultureInfo.InvariantCulture);
            viewModel.BuyDate = appModel.BuyDate.ToString(CultureInfo.InvariantCulture);
            viewModel.DateUpdated = appModel.DateUpdated.ToString(CultureInfo.InvariantCulture);

            return viewModel;
        }
    }
}