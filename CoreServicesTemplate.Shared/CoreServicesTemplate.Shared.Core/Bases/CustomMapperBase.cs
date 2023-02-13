using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public abstract class CustomMapperBase<TIn, TOut> : ICustomMapper<TIn, TOut>
    {
        private readonly IDefaultMapper<TIn, TOut> _defaultMapper;

        protected CustomMapperBase(IDefaultMapper<TIn, TOut> defaultMapper)
        {
            _defaultMapper = defaultMapper;
        }

        public virtual TOut Map(TIn @in)
        {
            TOut value = _defaultMapper.Map(@in);

            return value;
        }

        public virtual TIn Map(TOut @out)
        {
            TIn value = _defaultMapper.Map(@out);

            return value;
        }

        public virtual TOut Map(TIn @in, TOut @out)
        {
            TOut value = _defaultMapper.Map(@in, @out);

            return value;
        }

        public virtual TIn Map(TOut @out, TIn @in)
        {
            TIn value = _defaultMapper.Map(@out, @in);

            return value;
        }
    }
}
