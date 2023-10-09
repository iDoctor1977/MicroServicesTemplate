namespace CoreServicesTemplate.StorageRoom.Common.DomainModels.Wallet;

public class CreateWalletEventModel
{
    public Guid OwnerGuid { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
    public string Cap { get; set; }
    public string FromAddress { get; set; }
    public string ToAddress { get; set; }
}