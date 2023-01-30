﻿using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;

public interface IAddUserFeature : IQueryHandlerFeature<UserAppModel, OperationStatusResult> { }