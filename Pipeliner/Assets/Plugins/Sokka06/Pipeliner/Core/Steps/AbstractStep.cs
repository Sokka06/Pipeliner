using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public abstract class AbstractStep : IStep
    {
        public IStepParameters Parameters { get; }
        public float Progress { get; protected set; }

        public AbstractStep(IStepParameters parameters)
        {
            Parameters = parameters;
            Progress = 0f;
        }

        public abstract IEnumerator Run(Action<IStepResult> result);

        public virtual void OnAbort()
        {
            
        }
    }
}