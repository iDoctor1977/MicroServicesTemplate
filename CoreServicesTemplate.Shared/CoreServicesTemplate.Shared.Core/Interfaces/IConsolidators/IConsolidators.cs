namespace CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators
{
    public interface IConsolidators<in TIn, out TOut>
    {
        TOut ToData(TIn viewModel);
    }
}