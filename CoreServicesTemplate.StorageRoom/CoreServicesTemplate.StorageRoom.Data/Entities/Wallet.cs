namespace CoreServicesTemplate.StorageRoom.Data.Entities
{
    public class Wallet : EntityEfBase
    {
        public Guid OwnerGuid { get; set; }
        public decimal TradingAllowedBalance { get; set; }
        public decimal OperationAllowedBalance { get; set; }
        public decimal Balance { get; set; }
        public decimal Performance { get; set; }
        public virtual ICollection<WalletItem> ColWalletItems { get; set; }
    }
}
