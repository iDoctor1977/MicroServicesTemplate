using System.Collections.Generic;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

public interface IDefaultMapper<TIn, TOut>
{
    TOut Map(TIn viewModel);
    TIn Map(TOut appModel);
    TOut Map(TIn viewModel, TOut @out);
    TIn Map(TOut appModel, TIn @in);
    ICollection<TOut> Map(ICollection<TIn> sourceCollection);
    ICollection<TIn> Map(ICollection<TOut> sourceCollection);
}