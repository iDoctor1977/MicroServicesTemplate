using System.Net.Mail;

namespace CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;

public class EmailPropertiesAppModel : WalletAppModelBase
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
    public string Cap { get; set; }
    public MailAddress FromAddress { get; set; }
    public MailAddress ToAddress { get; set; }
}