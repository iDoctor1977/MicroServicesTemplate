namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDbContexts;

public interface IDbContextWrap
{
    void SaveChanges();
    Task SaveChangesAsync();
    void Dispose();
    Task DisposeAsync();
}