using System.ComponentModel.DataAnnotations;

namespace CoreServicesTemplate.Dashboard.Web.Models.Wallets;

public class CreateWalletViewModel : WalletViewBaseModel
{
    [Display(Name = "Day")]
    [DataType(DataType.Text)]
    [Required]
    public string DayTime { get; set; }
}