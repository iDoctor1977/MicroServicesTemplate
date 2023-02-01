using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

namespace CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;

public interface IAddUserFeature : ICommandHandler<UserAppModel> { }