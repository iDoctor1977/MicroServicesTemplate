using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet
{
    public class CreateWalletAppDto
    {
        public Guid OwnerGuid { get; set; }
        public decimal TradingAllowedBalance { get; set; }
        public decimal OperationAllowedBalance { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<WalletItemAppDto> WalletItems { get; set; }
    }
}
