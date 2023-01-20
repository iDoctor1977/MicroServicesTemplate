using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.ICqrsHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots
{
    public interface IAddUserDepot : IQueryHandlerCqrs<UserAppModel, OperationStatusResult> { }
}