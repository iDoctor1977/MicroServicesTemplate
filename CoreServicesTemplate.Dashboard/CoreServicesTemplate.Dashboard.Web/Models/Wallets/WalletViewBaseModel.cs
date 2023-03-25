using System.ComponentModel.DataAnnotations;

namespace CoreServicesTemplate.Dashboard.Web.Models.Wallets;

public class WalletViewBaseModel
{
    [Display(Name = "Balance")]
    [DataType(DataType.Text)]
    [Required]
    public string Balance { get; set; }

    [Display(Name = "Reading allowed balance")]
    [DataType(DataType.Text)]
    [Required]
    public string TradingAllowedBalance { get; set; }

    [Display(Name = "Operation allowed balance")]
    [DataType(DataType.Text)]
    [Required]
    public string OperationAllowedBalance { get; set; }
}