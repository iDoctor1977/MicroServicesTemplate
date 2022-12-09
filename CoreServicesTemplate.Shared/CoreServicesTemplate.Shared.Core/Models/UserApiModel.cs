﻿using System;
using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.Shared.Core.Models
{
    public class UserApiModel : AApiModelBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
    }
}