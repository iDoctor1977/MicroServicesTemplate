namespace CoreServicesTemplate.Shared.Core.Interfaces.IMappers
{
    public interface IMapperStandard
    {
        TOut Map<TIn, TOut>(TIn model);
        TOut Map<TIn, TOut>(TIn modelIn, TOut modelOut);
    }
}