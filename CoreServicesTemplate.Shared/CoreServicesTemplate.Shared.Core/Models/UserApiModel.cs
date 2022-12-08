﻿using System;

namespace CoreServicesTemplate.Shared.Core.Models
{
    public class UserApiModel
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
    }
}