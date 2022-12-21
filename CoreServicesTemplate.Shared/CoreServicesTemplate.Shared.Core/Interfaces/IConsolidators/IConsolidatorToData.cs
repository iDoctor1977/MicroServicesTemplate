namespace CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators
{
    public interface IConsolidatorToData<TIn, TOut>
    {
        IConsolidatorToResolve<TIn, TOut> ToData(TIn @in);
        IConsolidatorToResolveReversing<TIn, TOut> ToDataReverse(TOut @out);
    }
}