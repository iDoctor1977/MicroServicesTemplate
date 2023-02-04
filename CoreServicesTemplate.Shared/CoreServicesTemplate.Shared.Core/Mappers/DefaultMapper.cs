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
            var result = _mapper.Map<TOut>(@in);

            return result;
        }

        public TIn Map(TOut @out)
        {
            var result = _mapper.Map<TIn>(@out);

            return result;
        }

        public TOut Map(TIn @in, TOut @out)
        {
            TOut result = _mapper.Map(@in, @out);

            return result;
        }

        public TIn Map(TOut @out, TIn @in)
        {
            TIn result = _mapper.Map(@out, @in);

            return result;
        }
    }
}