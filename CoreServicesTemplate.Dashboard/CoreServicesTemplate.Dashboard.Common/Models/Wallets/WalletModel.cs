using CoreServicesTemplate.Dashboard.Common.Models.WalletItems;

namespace CoreServicesTemplate.Dashboard.Common.Models.Wallets;

public class WalletModel : WalletModelBase
{
    public ICollection<WalletItemModel> WalletItems { get; set; }
}