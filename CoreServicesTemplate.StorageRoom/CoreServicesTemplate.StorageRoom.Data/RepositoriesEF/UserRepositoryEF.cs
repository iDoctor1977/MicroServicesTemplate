using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Data.Bases;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.RepositoriesEF
{
    public class UserRepositoryEF : RepositoryBaseEF<User>, IUserRepository
    {
        public UserRepositoryEF(DbContextProject dbContext) : base(dbContext) { }

        public async Task UpdateEntity(User entity)
        {
            var updateEntity = await EntitySet.SingleOrDefaultAsync(e => e.Name == entity.Name);
            
            try
            {
                if (entity != null)
                {
                    updateEntity.Name = entity.Name;
                    updateEntity.Surname = entity.Surname;
                    updateEntity.Birth = entity.Birth;
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public async Task<User> ReadEntityByName(User entity)
        {
            try
            {
                entity = EntitySet.SingleOrDefault(e => e.Name == entity.Name);

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

        public async Task CreateEntity(User entity)
        {
            try
            {
                if (entity != null)
                {
                    entity.Id = new Random().Next();
                    EntitySet.Add(entity);
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            await Task.CompletedTask;
        }

        public async Task<User> ReadEntityByGuid(User entity)
        {
            try
            {
                entity = await EntitySet.SingleOrDefaultAsync(e => e.Guid == entity.Guid);

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

        public async Task DeleteEntity(User entity)
        {
            try
            {
                entity = await EntitySet.FindAsync(entity.Guid);

                if (entity != null)
                {
                    EntitySet.Remove(entity);
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }
        }

        public async Task<IEnumerable<User>> ReadEntities()
        {
            try
            {
                IEnumerable<User> entities = EntitySet.ToList();

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
