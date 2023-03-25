﻿using System.Linq.Expressions;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories
{
    public interface IRepository<T> : IRepository where T : class
    {
        T? Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    }

    public interface IRepository { }
}
