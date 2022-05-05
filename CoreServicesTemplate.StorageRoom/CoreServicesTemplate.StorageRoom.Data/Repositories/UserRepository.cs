using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IServiceProvider service) : base(service) { }

        public UserRepository(IServiceProvider service, string dbName) : base(service, dbName) { }

        public UserRepository(IServiceProvider service, DbContextOptions<ProjectDbContext> options) : base(service, options) { }

        public async Task<int> CreateEntity(UserApiModel model)
        {
            var entity = Mapper.Map<User>(model);

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

        public async Task<int> UpdateEntity(UserApiModel model)
        {
            var entity = await DbContext.EntitiesA.FindAsync(model.Guid);

            try
            {
                if (entity != null)
                {
                    entity.Name = model.Name;
                    entity.Surname = model.Surname;

                    return await Commit();
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            return 0;
        }

        public async Task<UserApiModel> ReadEntityByGuid(Guid guid)
        {
            try
            {
                var entity = await DbContext.EntitiesA.FindAsync(guid);

                if (entity != null)
                {
                    var model = Mapper.Map<UserApiModel>(entity);

                    return await Task.FromResult(model);
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            return await Task.FromResult<UserApiModel>(null);
        }

        public async Task<UserApiModel> ReadEntityByName(string name)
        {
            try
            {
                var entity = DbContext.EntitiesA.SingleOrDefault(eA => eA.Name == name);

                if (entity != null)
                {
                    var model = Mapper.Map<UserApiModel>(entity);

                    return await Task.FromResult(model);
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            return await Task.FromResult<UserApiModel>(null);
        }

        public async Task<int> DeleteEntity(UserApiModel model)
        {
            try
            {
                var entity = DbContext.EntitiesA.Find(model.Guid);

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

        public async Task<UsersApiModel> ReadEntities()
        {
            var model = new UsersApiModel();

            try
            {
                IEnumerable<User> entities = DbContext.EntitiesA.ToList();

                if (entities.Any())
                {
                    model.UsersApiModelList = Mapper.Map<IEnumerable<UserApiModel>>(entities);

                    return await Task.FromResult(model);
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            return await Task.FromResult(model);
        }
    }
}
