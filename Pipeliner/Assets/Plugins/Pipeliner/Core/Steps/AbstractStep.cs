using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public class AbstractStep : IStep
    {
        public IStepParameters Parameters { get; }
        public EventValue<float> Progress { get; }

        protected bool _abortRequested;

        public AbstractStep(IStepParameters parameters)
        {
            Parameters = parameters;
            Progress = new EventValue<float>(0f);
        }

        public virtual IEnumerator Run(Action<IStepResult> result)
        {
            Progress.Value = 0f;
            
            result?.Invoke(new IStepResult.Success());
            return null;
        }

        public virtual void Abort()
        {
            _abortRequested = true;
        }
    }
}