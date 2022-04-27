namespace CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers
{
    public interface ICustomMapper
    {
        TOut Map<TIn, TOut>(TIn model);
    }
}