using AutoMapper;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Shared.Core.Mappers
{
    /// <summary>
    /// Wrapper class for external mapping tool.
    /// </summary>
    public class MapperWrap : IMapperWrap
    {
        private readonly IMapper _mapper;

        public MapperWrap(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TOut Map<TIn ,TOut>(TIn model)
        {
            return _mapper.Map<TOut>(model);
        }

        public TOut Map<TIn, TOut>(TIn modelIn, TOut modelOut)
        {
            return _mapper.Map(modelIn, modelOut);
        }
    }
}