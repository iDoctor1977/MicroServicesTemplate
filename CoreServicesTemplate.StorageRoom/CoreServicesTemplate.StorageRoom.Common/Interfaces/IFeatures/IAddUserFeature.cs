using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;

public interface IAddUserFeature : ICommandHandler<UserAppModel> { }