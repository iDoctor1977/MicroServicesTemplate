﻿using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Dashboard.Common.Models;

public class UsersAppModel : IAppModel
{
    public IEnumerable<UserAppModel> UsersModelList { get; set; }
}