using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.WalletItems;

namespace CoreServicesTemplate.Dashboard.Common.Models.DomainModels.Wallets;

public class WalletModel : WalletModelBase
{
    public ICollection<WalletItemModel> WalletItems { get; set; }
}