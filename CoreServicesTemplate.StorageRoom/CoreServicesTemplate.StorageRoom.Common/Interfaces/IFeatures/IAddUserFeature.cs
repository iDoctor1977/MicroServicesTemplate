﻿using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;

public interface IAddUserFeature : IQueryHandler<UserAppModel, OperationStatusResult> { }