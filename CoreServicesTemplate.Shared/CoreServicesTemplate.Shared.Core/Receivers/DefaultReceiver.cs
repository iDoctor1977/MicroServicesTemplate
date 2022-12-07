using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Shared.Core.Receivers
{
    public sealed class DefaultReceiver<TIn, TOut> : AConsolidatorBase<TIn, TOut>
    {
        public DefaultReceiver(ICustomMapper customMapper) : base(customMapper) { }

        public override TOut ToData(TIn viewModel)
        {
            var model = ToExternalData(viewModel);

            return model;
        }
    }
}