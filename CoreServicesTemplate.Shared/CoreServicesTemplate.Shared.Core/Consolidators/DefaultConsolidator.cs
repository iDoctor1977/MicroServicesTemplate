using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Shared.Core.Consolidators
{
    public sealed class DefaultConsolidator<TIn, TOut> : AConsolidatorBase<TIn, TOut>
    {
        public DefaultConsolidator(ICustomMapper customMapper) : base(customMapper) { }

        public override TOut ToData(TIn modelIn)
        {
            var model = ToExternalData(modelIn);

            return model;
        }
    }
}