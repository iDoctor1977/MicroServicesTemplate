using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public abstract class AConsolidatorBase<TIn, TOut> : IConsolidators<TIn, TOut>
    {
        private readonly ICustomMapper _customMapper;

        protected AConsolidatorBase(ICustomMapper customMapper)
        {
            _customMapper = customMapper;
        }

        protected TOut ToExternalData(TIn model)
        {
            var valueMap = _customMapper.Map<TIn, TOut>(model);

            return valueMap;
        }

        public IEnumerable<TOut> ToData(IEnumerable<TIn> modelIn)
        {
            var models = new List<TOut>();

            foreach (var apiModel in modelIn)
            {
                models.Add(ToData(apiModel));
            }

            return models;
        }

        public abstract TOut ToData(TIn modelIn);
    }
}