using System.Collections.Generic;
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
        /// <param name="viewModel">Source object to map from</param>
        /// <returns>Mapped destination object</returns>
        public TOut Map(TIn viewModel)
        {
            var result = _mapper.Map<TOut>(viewModel);

            return result;
        }

        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// The source type is inferred from the source object.
        /// </summary>
        /// <param name="appModel">Source object to map from</param>
        /// <returns>Mapped destination object</returns>
        public TIn Map(TOut appModel)
        {
            var result = _mapper.Map<TIn>(appModel);

            return result;
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object.
        /// </summary>
        /// <param name="viewModel">Source type to use</param>
        /// <param name="out">Destination type</param>
        /// <returns>Mapped destination object</returns>
        public TOut Map(TIn viewModel, TOut @out)
        {
            TOut result = _mapper.Map(viewModel, @out);

            return result;
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object.
        /// </summary>
        /// <param name="appModel">Source type to use</param>
        /// <param name="in">Destination type</param>
        /// <returns>Mapped destination object</returns>
        public TIn Map(TOut appModel, TIn @in)
        {
            TIn result = _mapper.Map(appModel, @in);

            return result;
        }

        /// <summary>
        /// Executes the mapping of the source collection to the destination collection.
        /// </summary>
        /// <param name="sourceCollection">Source collection.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ICollection<TIn> Map(ICollection<TOut> sourceCollection)
        {
            var result = _mapper.Map<ICollection<TIn>>(sourceCollection);

            return result;
        }

        /// <summary>
        /// Executes the mapping of the source collection to the destination collection.
        /// </summary>
        /// <param name="sourceCollection">Source collection.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ICollection<TOut> Map(ICollection<TIn> sourceCollection)
        {
            var result = _mapper.Map<ICollection<TOut>>(sourceCollection);

            return result;
        }
    }
}