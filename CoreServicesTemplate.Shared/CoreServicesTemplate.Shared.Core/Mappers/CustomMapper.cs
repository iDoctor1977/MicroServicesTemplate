using AutoMapper;
using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Shared.Core.Mappers
{
    /// <summary>
    /// Wrapper class for external mapping tool.
    /// </summary>
    public class CustomMapper : ICustomMapper
    {
        private readonly IMapper _mapper;

        public CustomMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void MapAggregate(IAggModel aggModel, IAggregate aggClass)
        {
            _mapper.Map(aggModel, aggClass);
        }

        public TOut Map<TIn ,TOut>(TIn model)
        {
            return _mapper.Map<TOut>(model);
        }

        public TIn ReverseMap<TOut, TIn>(TOut model)
        {
            return _mapper.Map<TIn>(model);
        }
    }
}