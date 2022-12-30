namespace CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators
{
    public interface IConsolidator<TIn, TOut>
    {
        IConsolidatorToResolve<TIn, TOut> ToData(TIn @in);
        IConsolidatorToResolveReversing<TIn, TOut> ToDataReverse(TOut @out);
    }
}