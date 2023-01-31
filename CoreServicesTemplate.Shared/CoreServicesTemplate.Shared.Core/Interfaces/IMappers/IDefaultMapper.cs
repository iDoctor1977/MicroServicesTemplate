namespace CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

public interface IDefaultMapper<TIn, TOut>
{
    TOut Map(TIn @in);
    TIn Map(TOut @out);
    TOut Map(TIn @in, TOut @out);
}