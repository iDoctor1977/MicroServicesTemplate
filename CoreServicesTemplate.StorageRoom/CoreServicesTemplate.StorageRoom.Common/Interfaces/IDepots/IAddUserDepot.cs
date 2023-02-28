using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.AggModels;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots
{
    public interface IAddUserDepot : ICommandHandler<UserAggModel> { }
}