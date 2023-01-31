using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IResolveMappers;

namespace CoreServicesTemplate.Shared.Core.Bases;

public abstract class ACustomResolveMapperBase<TIn, TOut> : AResolveMapperBase<TIn, TOut>,
    IResolveMapperToResolve<TIn, TOut>,
    IResolveMapperToResolveReversing<TIn, TOut> where TOut : new() where TIn : new()
{
    protected ACustomResolveMapperBase(ICustomMapper customMapper) : base(customMapper)
    {
        ModelIn = new TIn();
        ModelOut = new TOut();
    }

    protected TIn ModelIn { get; set; }
    protected TOut ModelOut { get; set; }

    TOut IResolveMapperToResolve<TIn, TOut>.Resolve()
    {
        return ModelOut;
    }

    TIn IResolveMapperToResolveReversing<TIn, TOut>.Resolve()
    {
        return ModelIn;
    }
}