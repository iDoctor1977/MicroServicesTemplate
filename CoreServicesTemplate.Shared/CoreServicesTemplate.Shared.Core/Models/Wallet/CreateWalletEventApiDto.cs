using System.Net.Mail;

namespace CoreServicesTemplate.Shared.Core.Models.Wallet
{
    public class CreateWalletEventApiDto : WalletApiBaseDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Cap { get; set; }
        public MailAddress FromAddress { get; set; }
        public MailAddress ToAddress { get; set; }
    }
}