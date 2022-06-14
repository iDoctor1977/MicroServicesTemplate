using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Data.Bases
{
    public abstract class ADepotBase
    {
        protected readonly IMapper Mapper;

        protected ADepotBase(IServiceProvider service)
        {
            Mapper = service.GetRequiredService<IMapper>();
        }
    }
}