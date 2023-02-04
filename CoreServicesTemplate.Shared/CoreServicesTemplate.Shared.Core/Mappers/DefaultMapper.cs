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

        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// The source type is inferred from the source object.
        /// </summary>
        /// <param name="in">Source object to map from</param>
        /// <returns>Mapped destination object</returns>
        public TOut Map(TIn @in)
        {
            var result = _mapper.Map<TOut>(@in);

            return result;
        }

        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// The source type is inferred from the source object.
        /// </summary>
        /// <param name="out">Source object to map from</param>
        /// <returns>Mapped destination object</returns>
        public TIn Map(TOut @out)
        {
            var result = _mapper.Map<TIn>(@out);

            return result;
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object.
        /// </summary>
        /// <param name="in">Source type to use</param>
        /// <param name="out">Destination type</param>
        /// <returns>Mapped destination object</returns>
        public TOut Map(TIn @in, TOut @out)
        {
            TOut result = _mapper.Map(@in, @out);

            return result;
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object.
        /// </summary>
        /// <param name="out">Source type to use</param>
        /// <param name="in">Destination type</param>
        /// <returns>Mapped destination object</returns>
        public TIn Map(TOut @out, TIn @in)
        {
            TIn result = _mapper.Map(@out, @in);

            return result;
        }
    }
}