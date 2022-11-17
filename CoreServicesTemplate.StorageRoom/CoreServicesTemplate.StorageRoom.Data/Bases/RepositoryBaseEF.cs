using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.Bases
{
    public class RepositoryBaseEF<T> : IRepository<T> where T : EntityBase
    {
        protected RepositoryBaseEF (DbContextProject dbContext) {
            DbContext = dbContext;
            EntitySet = DbContext.Set<T>();
        }

        protected DbContextProject DbContext { get; }
        protected DbSet<T> EntitySet { get; }

        public T Get(Expression<Func<T, bool>> expression) => EntitySet.FirstOrDefault(expression);

        public IEnumerable<T> GetAll() => EntitySet.AsEnumerable();

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression) => EntitySet.Where(expression).AsEnumerable();

        public void Add(T entity) => DbContext.Add(entity);

        public async Task AddAsync(T entity) => await DbContext.AddAsync(entity);

        public void AddRange(IEnumerable<T> entities) => DbContext.AddRange(entities);

        public void Remove(T entity) => DbContext.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) => DbContext.RemoveRange(entities);

        public void Update(T entity) => DbContext.Update(entity);

        public void UpdateRange(IEnumerable<T> entities) => DbContext.UpdateRange(entities);

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) => await EntitySet.FirstOrDefaultAsync(expression, cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) => await EntitySet.ToListAsync(cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) => await EntitySet.Where(expression).ToListAsync(cancellationToken);

        public async Task AddAsync(T entity, CancellationToken cancellationToken) => await DbContext.AddAsync(entity, cancellationToken);

        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            DbContext.AddRange(entities);
            return Task.CompletedTask;
        }
    }
}
