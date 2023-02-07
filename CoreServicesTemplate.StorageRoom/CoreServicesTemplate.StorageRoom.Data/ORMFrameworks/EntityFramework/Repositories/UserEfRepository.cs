using System.Reflection;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories
{
    public class UserEfRepository : EfRepositoryBase<User>, IUserRepository
    {
        public UserEfRepository(StorageRoomDbContext dbContext) : base(dbContext) { }

        public async Task<User> GetByNameAsync(User? entity)
        {
            try
            {
                entity = EntitySet.SingleOrDefault(e => entity != null && e.Name == entity.Name);

                if (entity != null)
                {
                    return await Task.FromResult(entity);
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod()?.Name, exception);
            }

            return await Task.FromResult(new User());
        }

        public async Task<OperationStatusResult> AddCustomAsync(User? entity)
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
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod()?.Name, exception);
            }

            return await Task.FromResult(OperationStatusResult.Created);
        }

        public async Task<User> GetByGuidAsync(User? entity)
        {
            try
            {
                entity = await EntitySet.SingleOrDefaultAsync(e => entity != null && e.Guid == entity.Guid);

                if (entity != null)
                {
                    return await Task.FromResult(entity);
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod()?.Name, exception);
            }

            return await Task.FromResult(new User());
        }

        public async Task<User> GetByIdAsync(User? entity)
        {
            try
            {
                entity = await EntitySet.SingleOrDefaultAsync(e => entity != null && e.Id == entity.Id);

                if (entity != null)
                {
                    return await Task.FromResult(entity);
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod()?.Name, exception);
            }

            return await Task.FromResult(new User());
        }

        public async Task<OperationStatusResult> DeleteCustomAsync(User? entity)
        {
            try
            {
                if (entity != null)
                {
                    entity = await EntitySet.FindAsync(entity.Guid);

                    if (entity != null)
                    {
                        EntitySet.Remove(entity);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod()?.Name, exception);
            }

            return await Task.FromResult(OperationStatusResult.Deleted);
        }

        public async Task<IEnumerable<User>> GetAllCustomAsync()
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
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod()?.Name, exception);
            }

            return await Task.FromResult(Enumerable.Empty<User>());
        }

        public async Task<OperationStatusResult> UpdateCustomAsync(User? entity)
        {
            var updateEntity = await EntitySet.SingleOrDefaultAsync(e => entity != null && e.Name == entity.Name);

            try
            {
                if (entity != null && updateEntity != null)
                {
                    updateEntity.Name = entity.Name;
                    updateEntity.Surname = entity.Surname;
                    updateEntity.Birth = entity.Birth;
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod()?.Name, exception);
            }

            return await Task.FromResult(OperationStatusResult.Updated);
        }
    }
}
