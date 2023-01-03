namespace CoreServicesTemplate.Shared.Core.Interfaces.IMappers
{
    public interface ICustomMapper
    {
        TOut Map<TIn, TOut>(TIn model);
        TIn ReverseMap<TOut, TIn>(TOut model);
    }
}