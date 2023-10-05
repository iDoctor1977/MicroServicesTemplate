namespace CoreServicesTemplate.StorageRoom.Common.DomainModels.Wallet
{
    public class CreateWalletModel : BaseWalletModel
    {
        public decimal Balance { get; set; }
        public decimal TradingAllowedBalance { get; set; }
        public decimal OperationAllowedBalance { get; set; }
    }
}
