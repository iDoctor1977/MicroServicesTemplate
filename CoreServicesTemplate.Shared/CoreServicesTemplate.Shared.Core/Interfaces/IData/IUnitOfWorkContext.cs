using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IData;

public interface IUnitOfWorkContext
{
    void Commit();
    Task CommitAsync();
}