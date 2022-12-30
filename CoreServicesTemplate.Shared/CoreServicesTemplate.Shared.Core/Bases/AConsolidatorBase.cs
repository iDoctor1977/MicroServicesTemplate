using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Shared.Core.Bases;

public abstract class AConsolidatorBase<TIn, TOut> : IConsolidator<TIn, TOut>
{
    private readonly ICustomMapper _customMapper;

    protected AConsolidatorBase(ICustomMapper customMapper)
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

    public abstract IConsolidatorToResolve<TIn, TOut> ToData(TIn @in);
    public abstract IConsolidatorToResolveReversing<TIn, TOut> ToDataReverse(TOut @out);
}