using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Shared.Core.Mappers;

public class DefaultMapper<TIn, TOut> : IMapperService<TIn, TOut>
{
    private readonly IMapperWrap _mapper;

    public DefaultMapper(IMapperWrap mapper)
    {
        _mapper = mapper;
    }

    public TOut Map(TIn @in)
    {
        TOut valueMap = _mapper.Map<TIn, TOut>(@in);

        return valueMap;
    }

    public TIn Map(TOut @out)
    {
        TIn valueMap = _mapper.Map<TOut, TIn>(@out);

        return valueMap;
    }

    public TOut Map(TIn @in, TOut @out)
    {
        TOut valueMap = _mapper.Map(@in, @out);

        return valueMap;
    }
}