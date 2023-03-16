namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;

public interface IAppDbContext
{
    void Commit();
    Task CommitAsync();
}