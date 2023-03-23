using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using System.Collections.Generic;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public abstract class CustomMapperBase<TIn, TOut> : ICustomMapper<TIn, TOut>
    {
        private readonly IDefaultMapper<TIn, TOut> _defaultMapper;

        protected CustomMapperBase(IDefaultMapper<TIn, TOut> defaultMapper)
        {
            _defaultMapper = defaultMapper;
        }

        public virtual TOut Map(TIn viewModel)
        {
            TOut value = _defaultMapper.Map(viewModel);

            return value;
        }

        public virtual TIn Map(TOut appModel)
        {
            TIn value = _defaultMapper.Map(appModel);

            return value;
        }

        public virtual TOut Map(TIn viewModel, TOut @out)
        {
            TOut value = _defaultMapper.Map(viewModel, @out);

            return value;
        }

        public virtual TIn Map(TOut appModel, TIn @in)
        {
            TIn value = _defaultMapper.Map(appModel, @in);

            return value;
        }

        public virtual ICollection<TOut> Map(ICollection<TIn> sourceCollection)
        {
            return _defaultMapper.Map(sourceCollection);
        }

        public virtual ICollection<TIn> Map(ICollection<TOut> sourceCollection)
        {
            return _defaultMapper.Map(sourceCollection);
        }
    }
}
