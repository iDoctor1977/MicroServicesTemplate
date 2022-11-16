using System.Threading.Tasks;

namespace CoreServicesTemplate.StorageRoom.Data.Interfaces
{
    public interface IGenericDisposable
    {
        void Commit();
        Task CommitAsync();
        void Dispose();
        ValueTask DisposeAsync();
    }
}