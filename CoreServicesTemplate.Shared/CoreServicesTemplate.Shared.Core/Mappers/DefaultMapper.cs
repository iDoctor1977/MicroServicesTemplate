using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Shared.Core.Mappers;

public class DefaultMapper<TIn, TOut> : IDefaultMapper<TIn, TOut>
{
    private readonly IMapperStandard _mapper;

    public DefaultMapper(IMapperStandard mapper)
    {
        _mapper = mapper;
    }

    public TOut Map(TIn @in)
    {
        var valueMap = _mapper.Map<TIn, TOut>(@in);

        return valueMap;
    }

    public TIn Map(TOut @out)
    {
        var valueMap = _mapper.Map<TOut, TIn>(@out);

        return valueMap;
    }

    public TOut Map(TIn @in, TOut @out)
    {
        var valueMap = _mapper.Map(@in, @out);

        return valueMap;
    }
}