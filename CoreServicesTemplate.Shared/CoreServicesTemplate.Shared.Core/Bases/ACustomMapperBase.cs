using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Shared.Core.Bases;

public abstract class ACustomMapperBase<TIn, TOut> : IMapperService<TIn, TOut>
{
    private readonly IMapperWrap _mapperWrap;

    protected ACustomMapperBase(IMapperWrap mapperWrap)
    {
        _mapperWrap = mapperWrap;
    }

    protected TOut DataInToDataOut(TIn @in)
    {
        var tOut = _mapperWrap.Map<TIn, TOut>(@in);

        return tOut;
    }
    protected TIn DataOutToDataIn(TOut @out)
    {
        var tIn = _mapperWrap.Map<TOut, TIn>(@out);

        return tIn;
    }

    protected TOut ToDataOut(TIn @in, TOut @out)
    {
        var tOut = _mapperWrap.Map(@in, @out);

        return tOut;
    }

    public abstract TOut Map(TIn @in);
    public abstract TIn Map(TOut @out);
    public abstract TOut Map(TIn @in, TOut @out);
}