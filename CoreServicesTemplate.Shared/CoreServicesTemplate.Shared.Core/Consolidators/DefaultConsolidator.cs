using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Shared.Core.Consolidators;

public sealed class DefaultConsolidator<TIn, TOut> : AConsolidatorBase<TIn, TOut>,
    IConsolidatorToResolve<TIn, TOut>,
    IConsolidatorToResolveReversing<TIn,TOut>
{
    private TIn _modelIn;
    private TOut _modelOut;

    public DefaultConsolidator(ICustomMapper customMapper) : base(customMapper) { }

    public override IConsolidatorToResolve<TIn, TOut> ToData(TIn @in)
    {
        _modelIn = @in;
        _modelOut = InDataToOutData(@in);

        return this;
    }

    public override IConsolidatorToResolveReversing<TIn, TOut> ToDataReverse(TOut @out)
    {
        _modelIn = OutDataToInData(@out);

        return this;
    }

    TOut IConsolidatorToResolve<TIn, TOut>.Resolve()
    {
        return _modelOut;
    }

    TIn IConsolidatorToResolveReversing<TIn, TOut>.Resolve()
    {
        return _modelIn;
    }
}