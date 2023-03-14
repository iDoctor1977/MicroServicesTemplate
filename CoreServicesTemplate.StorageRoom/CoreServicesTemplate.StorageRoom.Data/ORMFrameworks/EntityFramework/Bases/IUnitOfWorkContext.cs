namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;

public interface IUnitOfWorkContext
{
    void Commit();
    Task CommitAsync();
}