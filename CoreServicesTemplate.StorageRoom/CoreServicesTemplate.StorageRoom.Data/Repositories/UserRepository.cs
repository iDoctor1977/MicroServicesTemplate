using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Data.Bases;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IServiceProvider service) : base(service) { }

        public UserRepository(IServiceProvider service, string dbName) : base(service, dbName) { }

        public UserRepository(IServiceProvider service, DbContextOptions<ProjectDbContext> options) : base(service, options) { }

        public async Task<int> CreateEntity(User entity)
        {
            try
            {
                if (entity != null)
                {
                    entity.Id = new Random().Next();
                    DbContext.EntitiesA.Add(entity);

                    return await Commit();
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            return 0;
        }

        public async Task<int> UpdateEntity(User entity)
        {
            var updateEntity = await DbContext.EntitiesA.SingleOrDefaultAsync(e => e.Name == entity.Name);

            try
            {
                if (entity != null)
                {
                    updateEntity.Name = entity.Name;
                    updateEntity.Surname = entity.Surname;
                    updateEntity.Birth = entity.Birth;

                    return await Commit();
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            return 0;
        }

        public async Task<User> ReadEntityByGuid(User entity)
        {
            try
            {
                entity = await DbContext.EntitiesA.SingleOrDefaultAsync(e => e.Guid == entity.Guid);

                if (entity != null)
                {
                    return await Task.FromResult(entity);
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            return await Task.FromResult<User>(null);
        }

        public async Task<User> ReadEntityByName(User entity)
        {
            try
            {
                entity = DbContext.EntitiesA.SingleOrDefault(e => e.Name == entity.Name);

                if (entity != null)
                {
                    return await Task.FromResult(entity);
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            return await Task.FromResult<User>(null);
        }

        public async Task<int> DeleteEntity(User entity)
        {
            try
            {
                entity = await DbContext.EntitiesA.FindAsync(entity.Guid);

                if (entity != null)
                {
                    DbContext.EntitiesA.Remove(entity);

                    return await Commit();
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            return 0;
        }

        public async Task<IEnumerable<User>> ReadEntities()
        {
            try
            {
                IEnumerable<User> entities = DbContext.EntitiesA.ToList();

                if (entities.Any())
                {
                    return entities;
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            return await Task.FromResult(Enumerable.Empty<User>());
        }
    }
}
