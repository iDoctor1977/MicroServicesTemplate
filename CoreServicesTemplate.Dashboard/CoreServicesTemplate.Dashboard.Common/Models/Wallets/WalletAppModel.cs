using CoreServicesTemplate.Dashboard.Common.Models.WalletItems;

namespace CoreServicesTemplate.Dashboard.Common.Models.Wallets
{
    public class WalletAppModel : WalletAppBaseModel
    {
        public ICollection<WalletItemAppModel> WalletItems { get; set; }
    }
}
