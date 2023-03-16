using System.Linq.Expressions;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly AppDbContext _dbContext;

        protected EfRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            EntitySet = _dbContext.Set<T>();
        }

        private DbSet<T> EntitySet { get; }

        public T? Get(Expression<Func<T, bool>> expression) => EntitySet.FirstOrDefault(expression);

        public IEnumerable<T> GetAll() => EntitySet.AsEnumerable();

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression) => EntitySet.Where(expression).AsEnumerable();

        public void Add(T entity) => _dbContext.Add(entity);

        public async Task AddAsync(T entity) => await _dbContext.AddAsync(entity);

        public void AddRange(IEnumerable<T> entities) => _dbContext.AddRange(entities);

        public void Remove(T entity) => _dbContext.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) => _dbContext.RemoveRange(entities);

        public void Update(T entity) => _dbContext.Update(entity);

        public void UpdateRange(IEnumerable<T> entities) => _dbContext.UpdateRange(entities);

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) => await EntitySet.FirstOrDefaultAsync(expression, cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) => await EntitySet.ToListAsync(cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) => await EntitySet.Where(expression).ToListAsync(cancellationToken);

        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.AddRange(entities);
            return Task.CompletedTask;
        }
    }
}
