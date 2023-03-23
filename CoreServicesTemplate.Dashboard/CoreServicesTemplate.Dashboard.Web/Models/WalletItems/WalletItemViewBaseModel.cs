using System.ComponentModel.DataAnnotations;

namespace CoreServicesTemplate.Dashboard.Web.Models.WalletItems;

public class WalletItemViewBaseModel
{
    [Display(Name = "Amount")]
    [DataType(DataType.Text)]
    public string Amount { get; set; }

    [Display(Name = "Buy price")]
    [DataType(DataType.Text)]
    public string BuyPrice { get; set; }

    [Display(Name = "Quantity")]
    public int Quantity { get; set; }

    [Display(Name = "Buy date")]
    [DataType(DataType.Text)]
    public string BuyDate { get; set; }

    [Display(Name = "Date updated")]
    [DataType(DataType.Text)]
    public string DateUpdated { get; set; }
}