using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IMappers
{
    public interface ICustomMapper
    {
        void MapAggregate(IAggModel aggModel, IAggregate aggClass);

        TOut Map<TIn, TOut>(TIn model);
        TIn ReverseMap<TOut, TIn>(TOut model);
    }
}