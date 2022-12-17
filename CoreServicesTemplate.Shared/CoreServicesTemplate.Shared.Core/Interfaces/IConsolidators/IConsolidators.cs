using System.Collections.Generic;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators
{
    public interface IConsolidators<in TIn, out TOut>
    {
        TOut ToData(TIn modelIn);
        IEnumerable<TOut> ToData(IEnumerable<TIn> modelIn);
    }
}