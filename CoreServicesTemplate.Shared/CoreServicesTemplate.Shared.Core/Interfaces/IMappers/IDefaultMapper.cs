using System.Collections.Generic;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

public interface IDefaultMapper<TIn, TOut>
{
    TOut Map(TIn @in);
    TIn Map(TOut @out);
    TOut Map(TIn @in, TOut @out);
    TIn Map(TOut @out, TIn @in);
    ICollection<TOut> Map(ICollection<TIn> sourceCollection);
    ICollection<TIn> Map(ICollection<TOut> sourceCollection);
}