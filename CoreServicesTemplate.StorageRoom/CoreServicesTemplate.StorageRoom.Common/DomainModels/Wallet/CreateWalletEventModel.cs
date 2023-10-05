using System.Net.Mail;

namespace CoreServicesTemplate.StorageRoom.Common.DomainModels.Wallet;

public class CreateWalletEventModel : BaseWalletModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
    public string Cap { get; set; }
    public MailAddress FromAddress { get; set; }
    public MailAddress ToAddress { get; set; }
}