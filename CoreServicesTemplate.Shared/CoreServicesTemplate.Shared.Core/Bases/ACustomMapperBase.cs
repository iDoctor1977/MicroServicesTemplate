using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Shared.Core.Bases;

public abstract class ACustomMapperBase<TIn, TOut> : IDefaultMapper<TIn, TOut>
{
    private readonly IMapperStandard _mapper;

    protected ACustomMapperBase(IMapperStandard mapper)
    {
        _mapper = mapper;
    }

    protected TOut DataInToDataOut(TIn @in)
    {
        var tOut = _mapper.Map<TIn, TOut>(@in);

        return tOut;
    }
    protected TIn DataOutToDataIn(TOut @out)
    {
        var tIn = _mapper.Map<TOut, TIn>(@out);

        return tIn;
    }

    protected TOut ToDataOut(TIn @in, TOut @out)
    {
        var tOut = _mapper.Map(@in, @out);

        return tOut;
    }

    public abstract TOut Map(TIn @in);
    public abstract TIn Map(TOut @out);
    public abstract TOut Map(TIn @in, TOut @out);
}