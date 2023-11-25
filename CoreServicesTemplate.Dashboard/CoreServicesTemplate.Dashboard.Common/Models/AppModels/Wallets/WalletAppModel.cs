using CoreServicesTemplate.Dashboard.Common.Models.AppModels.WalletItems;

namespace CoreServicesTemplate.Dashboard.Common.Models.AppModels.Wallets
{
    public class WalletAppModel : WalletAppModelBase
    {
        public ICollection<WalletItemAppModel> WalletItems { get; set; }
    }
}
