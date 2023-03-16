namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;

public interface IAppDbContext
{
    void Commit();
    Task CommitAsync();
}