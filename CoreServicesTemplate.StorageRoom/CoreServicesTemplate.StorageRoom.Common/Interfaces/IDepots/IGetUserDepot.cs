﻿using CoreServicesTemplate.Shared.Core.Interfaces.ICqrs;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots
{
    public interface IGetUserDepot : IQueryHandlerCqrs<UserAppModel, UserAppModel> { }
}