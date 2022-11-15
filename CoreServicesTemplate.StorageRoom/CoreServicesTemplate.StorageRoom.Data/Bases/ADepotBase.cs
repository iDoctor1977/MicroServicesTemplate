using AutoMapper;

namespace CoreServicesTemplate.StorageRoom.Data.Bases
{
    public abstract class ADepotBase
    {
        protected readonly IMapper Mapper;

        protected ADepotBase(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}