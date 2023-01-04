using System.ComponentModel.DataAnnotations;

namespace CoreServicesTemplate.Dashboard.Web.Models;

public class AddressViewModel
{
    [Display(Name = "Indirizzo 1")]
    [DataType(DataType.Text)]
    public string Address1 { get; set; }

    [Display(Name = "Indirizzo 2")]
    [DataType(DataType.Text)]
    public string Address2 { get; set; }

    [Display(Name = "Citta'")]
    [DataType(DataType.Text)]
    public string City { get; set; }

    [Display(Name = "Stato")]
    [DataType(DataType.Text)]
    public string State { get; set; }

    [Display(Name = "Codice postale")]
    [DataType(DataType.Text)]
    public string PostalCode { get; set; }
}