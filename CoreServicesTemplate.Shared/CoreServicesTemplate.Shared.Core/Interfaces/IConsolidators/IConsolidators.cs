using System.Collections.Generic;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators
{
    public interface IConsolidators<in TIn, out TOut>
    {
        TOut ToData(TIn viewModel);
        IEnumerable<TOut> ToData(IEnumerable<TIn> viewModel);
    }
}