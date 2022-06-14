using System;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.Bases
{
    public class RepositoryBase
    {
        protected RepositoryBase (IServiceProvider service) {
            DbContext = new ProjectDbContext();
        }

        protected RepositoryBase(IServiceProvider service, string dbName)
        {
            DbContext = new ProjectDbContext(dbName);
        }

        protected RepositoryBase(IServiceProvider service, DbContextOptions<ProjectDbContext> options)
        {
            DbContext = new ProjectDbContext(options);
        }

        protected ProjectDbContext DbContext { get; }

        protected async Task<int> Commit()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}
