using System.Linq.Expressions;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.Bases;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Mocks
{
    public class EfRepositoryBaseMock<T> : IRepository<T> where T : EntityBase
    {
        protected EfRepositoryBaseMock(StorageRoomDbContext dbContext)
        {
            StorageRoomDbContext = dbContext;
            EntitySet = StorageRoomDbContext.Set<T>();
        }

        private StorageRoomDbContext StorageRoomDbContext { get; }

        private DbSet<T> EntitySet { get; }

        public T Get(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
