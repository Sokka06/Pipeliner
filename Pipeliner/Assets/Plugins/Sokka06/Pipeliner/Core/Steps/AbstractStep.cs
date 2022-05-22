using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public abstract class AbstractStep : IStep
    {
        private float _progress;
        
        public IStepParameters Parameters { get; }
        public float Progress
        {
            get => _progress;
            protected set => _progress = Mathf.Clamp01(value);
        }

        public AbstractStep(IStepParameters parameters)
        {
            Parameters = parameters;
            Progress = 0f;
        }

        public abstract IEnumerator Run(Action<IStepResult> result);

        public virtual void OnAbort() { }
    }
}