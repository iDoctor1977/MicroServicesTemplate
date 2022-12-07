using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Shared.Core.Presenters
{
    public sealed class DefaultPresenter<TIn, TOut> : AConsolidatorBase<TIn, TOut>
    {
        public DefaultPresenter(ICustomMapper customMapper) : base(customMapper) { }

        public override TOut ToData(TIn model)
        {
            var viewModel = ToExternalData(model);

            return viewModel;
        }
    }
}