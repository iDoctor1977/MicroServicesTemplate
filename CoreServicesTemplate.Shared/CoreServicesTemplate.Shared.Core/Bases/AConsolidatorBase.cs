using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Shared.Core.Bases;

public abstract class AConsolidatorBase<TIn, TOut> : IConsolidatorToData<TIn, TOut>
{
    private readonly ICustomMapper _customMapper;

    protected AConsolidatorBase(ICustomMapper customMapper)
    {
        _customMapper = customMapper;
    }

    protected TOut InDataToOutData(TIn model)
    {
        var valueMap = _customMapper.Map<TIn, TOut>(model);

        return valueMap;
    }

    protected TIn OutDataToInData(TOut model)
    {
        var valueMap = _customMapper.ReverseMap<TOut, TIn>(model);

        return valueMap;
    }

    public abstract IConsolidatorToResolve<TIn, TOut> ToData(TIn @in);

    public abstract IConsolidatorToResolveReversing<TIn, TOut> ToDataReverse(TOut @out);
}