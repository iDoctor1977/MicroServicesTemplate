using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.Bases;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases
{
    public class EfRepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        protected EfRepositoryBase(Lazy<StorageRoomDbContext> dbContext)
        {
            StorageRoomDbContext = dbContext.Value;
            EntitySet = StorageRoomDbContext.Set<T>();
        }

        private StorageRoomDbContext StorageRoomDbContext { get; }
        protected DbSet<T> EntitySet { get; }

        public T Get(Expression<Func<T, bool>> expression) => EntitySet.FirstOrDefault(expression);

        public IEnumerable<T> GetAll() => EntitySet.AsEnumerable();

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression) => EntitySet.Where(expression).AsEnumerable();

        public void Add(T entity) => StorageRoomDbContext.Add(entity);

        public async Task AddAsync(T entity) => await StorageRoomDbContext.AddAsync(entity);

        public void AddRange(IEnumerable<T> entities) => StorageRoomDbContext.AddRange(entities);

        public void Remove(T entity) => StorageRoomDbContext.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) => StorageRoomDbContext.RemoveRange(entities);

        public void Update(T entity) => StorageRoomDbContext.Update(entity);

        public void UpdateRange(IEnumerable<T> entities) => StorageRoomDbContext.UpdateRange(entities);

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) => await EntitySet.FirstOrDefaultAsync(expression, cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) => await EntitySet.ToListAsync(cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) => await EntitySet.Where(expression).ToListAsync(cancellationToken);

        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            StorageRoomDbContext.AddRange(entities);
            return Task.CompletedTask;
        }
    }
}
