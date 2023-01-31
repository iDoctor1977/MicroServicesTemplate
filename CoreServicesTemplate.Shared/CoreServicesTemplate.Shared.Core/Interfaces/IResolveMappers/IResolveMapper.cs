namespace CoreServicesTemplate.Shared.Core.Interfaces.IResolveMappers
{
    public interface IResolveMapper<TIn, TOut>
    {
        IResolveMapperToResolve<TIn, TOut> ToData(TIn @in);
        IResolveMapperToResolveReversing<TIn, TOut> ToDataReverse(TOut @out);
    }
}