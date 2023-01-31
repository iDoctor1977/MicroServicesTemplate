using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IResolveMappers;

namespace CoreServicesTemplate.Shared.Core.ResolveMappers;

public sealed class DefaultResolveMapper<TIn, TOut> : AResolveMapperBase<TIn, TOut>,
    IResolveMapperToResolve<TIn, TOut>,
    IResolveMapperToResolveReversing<TIn,TOut>
{
    private TIn _modelIn;
    private TOut _modelOut;

    public DefaultResolveMapper(ICustomMapper customMapper) : base(customMapper) { }

    public override IResolveMapperToResolve<TIn, TOut> ToData(TIn @in)
    {
        _modelIn = @in;
        _modelOut = InDataToOutData(@in);

        return this;
    }

    public override IResolveMapperToResolveReversing<TIn, TOut> ToDataReverse(TOut @out)
    {
        _modelIn = OutDataToInData(@out);

        return this;
    }

    TOut IResolveMapperToResolve<TIn, TOut>.Resolve()
    {
        return _modelOut;
    }

    TIn IResolveMapperToResolveReversing<TIn, TOut>.Resolve()
    {
        return _modelIn;
    }
}