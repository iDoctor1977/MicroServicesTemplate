namespace CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

public interface IMapping<TIn, TOut>
{
    TOut Map(TIn @in);
    TIn Map(TOut @out);
    TOut Map(TIn @in, TOut @out);
}