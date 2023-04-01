namespace CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet
{
    public class WalletAppBaseDto
    {
        public Guid OwnerGuid { get; set; }
        public decimal TradingAllowedBalance { get; set; }
        public decimal OperationAllowedBalance { get; set; }
        public decimal Balance { get; set; }
    }
}
