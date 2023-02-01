using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;

public interface IGetUserFeature : IQueryHandler<UserAppModel, UserAppModel> { }