namespace CoreServicesTemplate.StorageRoom.Data.Entities
{
    public class WalletItem : EntityEfBase
    {
        public decimal Amount { get; set; }
        public decimal BuyPrice { get; set; }
        public DateTime BuyDate { get; set; }
        public int Quantity { get; set; }
        public DateTime DateUpdated { get; set; }

        public int ExtWalletId { get; set; }
        public virtual Wallet ExtWallet { get; set; }

        public string ExtTicker { get; set; }
        public Guid ExtMarketItemGuid { get; set; }
    }
}
