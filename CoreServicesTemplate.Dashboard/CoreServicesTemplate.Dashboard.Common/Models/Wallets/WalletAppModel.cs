using CoreServicesTemplate.Dashboard.Common.Models.WalletItems;

namespace CoreServicesTemplate.Dashboard.Common.Models.Wallets
{
    public class WalletAppModel : WalletAppModelBase
    {
        public ICollection<WalletItemAppModel> WalletItems { get; set; }
    }
}
