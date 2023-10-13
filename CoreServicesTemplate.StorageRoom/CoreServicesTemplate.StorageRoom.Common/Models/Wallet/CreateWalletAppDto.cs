namespace CoreServicesTemplate.StorageRoom.Common.Models.Wallet
{
    public class CreateWalletAppDto : WalletAppDtoBase
    {
        public decimal TradingAllowedBalance { get; set; }
        public decimal OperationAllowedBalance { get; set; }
        public decimal Balance { get; set; }
    }
}
