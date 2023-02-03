using AutoMapper;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Shared.Core.Mappers
{
    /// <summary>
    /// Wrapper class for external mapping tool.
    /// </summary>
    public class DefaultMapper<TIn, TOut> : IDefaultMapper<TIn, TOut>
    {
        private readonly IMapper _mapper;

        public DefaultMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TOut Map(TIn @in)
        {
            return _mapper.Map<TOut>(@in);
        }

        public TIn Map(TOut @out)
        {
            return _mapper.Map<TIn>(@out);
        }

        public TOut Map(TIn @in, TOut @out)
        {
            return _mapper.Map(@in, @out);
        }
    }
}