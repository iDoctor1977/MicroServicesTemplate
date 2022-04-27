using System;
using CoreServicesTemplate.Shared.Core.Bases;

namespace CoreServicesTemplate.Shared.Core.Receivers
{
    public sealed class DefaultReceiver<TIn, TOut> : ABaseConsolidator<TIn, TOut>
    {
        public DefaultReceiver(IServiceProvider service) : base(service) { }

        public override TOut ToData(TIn viewModel)
        {
            var model = ToExternalData(viewModel);

            return model;
        }
    }
}