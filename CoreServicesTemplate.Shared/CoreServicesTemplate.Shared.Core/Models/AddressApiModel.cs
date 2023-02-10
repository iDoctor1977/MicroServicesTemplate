using System.ComponentModel.DataAnnotations;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Shared.Core.Models;

public class AddressApiModel : IApiModel
{
    [Required]
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public string PostalCode { get; set; }
}