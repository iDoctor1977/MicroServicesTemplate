using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Bases;

public abstract class AFeatureCommandBase<TIn> : IFeatureCommand<TIn>, ICommandHandleAggregate where TIn : IAppModel
{
    protected TIn ModelApp { get; set; }

    public abstract Task HandleAsync();
    public abstract ICommandHandleAggregate SetModel(TIn model);
}