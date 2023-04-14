using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IData;

public interface IAppDbContext
{
    void Commit();
    Task CommitAsync();
}