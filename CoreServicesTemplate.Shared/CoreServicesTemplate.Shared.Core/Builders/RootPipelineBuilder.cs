﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Shared.Core.Builders
{
    public abstract class RootPipelineBuilder<TIn, TOut> : IBuildStep<TIn, TOut> where TIn : IAppModel where TOut : IAppModel
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

        protected abstract Task<TOut> HandleRootStepAsync(TIn appModel);

        public Task<TOut> ExecuteAsync(TIn modelApp)
        {
            _stepInput = HandleRootStepAsync(modelApp);

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

    public interface IRootStep<TIn, TOut> : ISubStep<TIn, TOut> where TIn : IAppModel where TOut : IAppModel
    {
        IBuildStep<TIn, TOut> AddSubStep(ISubStep<TIn, TOut> newStep);
    }

    public interface IBuildStep<TIn, TOut> : IRootStep<TIn, TOut> where TIn : IAppModel where TOut : IAppModel { }

    public interface ISubStep<in TIn, TOut> where TIn : IAppModel where TOut : IAppModel
    {
        Task<TOut> ExecuteAsync(TIn modelApp);
    }
}
