using CoreServicesTemplate.Shared.Core.Interfaces.ICqrsHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots
{
    public interface IGetUsersDepot : IQueryHandlerCqrs<UsersAppModel> { }
}