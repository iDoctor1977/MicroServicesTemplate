using System;
using System.ComponentModel.DataAnnotations;
using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.Shared.Core.Models;

public class UserApiModel : ApiModelBase
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public DateTime Birth { get; set; }
    [Required]
    public AddressApiModel AddressApiModel { get; set; }
}