using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Attributes;

namespace CoreServicesTemplate.Shared.Core.Builders
{
    public abstract class RootPipelineBuilder<TIn, TOut> : IBuildStep<TIn, TOut>
    {
        private readonly RootAttribute _localAttribute;
        private readonly List<ISubStep<TIn, TOut>> _root;
        private object _stepInput;

        protected RootPipelineBuilder()
        {
            _localAttribute = (RootAttribute)Attribute.GetCustomAttribute(GetType(), typeof(RootAttribute));
            _root = new List<ISubStep<TIn, TOut>>();
        }

        public IBuildStep<TIn, TOut> AddSubStep(ISubStep<TIn, TOut> newStep)
        {
            LeafAttribute leaf = (LeafAttribute)Attribute.GetCustomAttribute(newStep.GetType(), typeof(LeafAttribute));
            RootAttribute root = (RootAttribute)Attribute.GetCustomAttribute(newStep.GetType(), typeof(RootAttribute));

            if (root != null || (leaf != null && leaf.GetRootFatherName.Equals(GetType().Name)))
            {
                _localAttribute.AddStepNameToThree(newStep.GetType().Name);

                _root.Add(newStep);

                return this;
            }

            throw new Exception(newStep.GetType().Name + " it doesn't belong to the root " + GetType().Name + " or attribute was not found.");
        }

        protected abstract Task<TOut> HandleRootStepAsync(TIn aggregate);

        public Task<TOut> ExecuteAsync(TIn aggregate)
        {
            _stepInput = HandleRootStepAsync(aggregate);

            if (_root.Count != 0)
            {
                foreach (var step in _root)
                {
                    _stepInput = step.ExecuteAsync((TIn)_stepInput);
                }
            }

            return Task.FromResult((TOut)_stepInput);
        }
    }

    public interface IRootStep<TIn, TOut> : ISubStep<TIn, TOut>
    {
        IBuildStep<TIn, TOut> AddSubStep(ISubStep<TIn, TOut> newStep);
    }

    public interface IBuildStep<TIn, TOut> : IRootStep<TIn, TOut> { }

    public interface ISubStep<in TIn, TOut>
    {
        Task<TOut> ExecuteAsync(TIn aggregate);
    }
}
