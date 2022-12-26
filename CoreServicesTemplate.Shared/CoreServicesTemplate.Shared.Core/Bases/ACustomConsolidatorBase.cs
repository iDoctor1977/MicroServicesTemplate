using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Shared.Core.Bases;

public abstract class ACustomConsolidatorBase<TIn, TOut> : AConsolidatorBase<TIn, TOut>,
    IConsolidatorToResolve<TIn, TOut>,
    IConsolidatorToResolveReversing<TIn, TOut> where TOut : new() where TIn : new()
{
    protected ACustomConsolidatorBase(ICustomMapper customMapper) : base(customMapper)
    {
        ModelIn = new TIn();
        ModelOut = new TOut();
    }

    protected TIn ModelIn { get; set; }
    protected TOut ModelOut { get; set; }

    TOut IConsolidatorToResolve<TIn, TOut>.Resolve()
    {
        return ModelOut;
    }

    TIn IConsolidatorToResolveReversing<TIn, TOut>.Resolve()
    {
        return ModelIn;
    }
}