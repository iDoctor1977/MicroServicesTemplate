using System;
using System.Collections.Generic;
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

        protected abstract TOut ExecuteRootStep(TIn aggregate);

        public TOut Execute(TIn aggregate)
        {
            _stepInput = ExecuteRootStep(aggregate);

            if (_root.Count != 0)
            {
                foreach (var step in _root)
                {
                    _stepInput = step.Execute((TIn)_stepInput);
                }
            }

            return (TOut)_stepInput;
        }
    }

    public interface IRootStep<TIn, TOut> : ISubStep<TIn, TOut>
    {
        IBuildStep<TIn, TOut> AddSubStep(ISubStep<TIn, TOut> newStep);
    }

    public interface IBuildStep<TIn, TOut> : IRootStep<TIn, TOut> { }

    public interface ISubStep<in TIn, out TOut>
    {
        TOut Execute(TIn aggregate);
    }
}
