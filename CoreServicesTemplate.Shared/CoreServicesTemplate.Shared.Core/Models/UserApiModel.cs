using System;
using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.Shared.Core.Models;

public class UserApiModel : ApiModelBase
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birth { get; set; }

    public AddressApiModel AddressApiModel { get; set; }
}