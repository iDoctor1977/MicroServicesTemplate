using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IResolveMappers;

namespace CoreServicesTemplate.Shared.Core.Bases;

public abstract class AResolveMapperBase<TIn, TOut> : IResolveMapper<TIn, TOut>
{
    private readonly ICustomMapper _customMapper;

    protected AResolveMapperBase(ICustomMapper customMapper)
    {
        _customMapper = customMapper;
    }

    protected TOut InDataToOutData(TIn @in)
    {
        var valueMap = _customMapper.Map<TIn, TOut>(@in);

        return valueMap;
    }

    protected TIn OutDataToInData(TOut @out)
    {
        var valueMap = _customMapper.ReverseMap<TOut, TIn>(@out);

        return valueMap;
    }

    public abstract IResolveMapperToResolve<TIn, TOut> ToData(TIn @in);
    public abstract IResolveMapperToResolveReversing<TIn, TOut> ToDataReverse(TOut @out);
}