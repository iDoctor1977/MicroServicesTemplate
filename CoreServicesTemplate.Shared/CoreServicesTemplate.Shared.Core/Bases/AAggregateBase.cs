using System;
using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;
using CoreServicesTemplate.Shared.Core.Resources;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public abstract class AAggregateBase<T> : IAggregate<T> where T : IAppModel
    {
        public T Model { get; set; }

        protected AAggregateBase(T model)
        {
            Model = model;

            if (!IsModelValid())
            {
                throw new ApplicationException($"{ErrorMessages.ABaseAggregate_ExceptionErrorString} EDF358B9-A42A-43F5-BAE4-5B67168D810A");
            }
        }

        public abstract T ToModel();
        public abstract bool IsModelValid();
    }
}
