using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots
{
    public interface IGetUserDepot : IQueryHandler<UserAppModel, UserAppModel> { }
}