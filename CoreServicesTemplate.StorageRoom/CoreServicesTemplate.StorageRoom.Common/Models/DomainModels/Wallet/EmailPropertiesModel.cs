namespace CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.Wallet;

public class EmailPropertiesModel
{
    public Guid OwnerGuid { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
    public string Cap { get; set; }
    public string FromAddress { get; set; }
    public string ToAddress { get; set; }
}