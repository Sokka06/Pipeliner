using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public interface IStepParameters
    {
        
    }
    
    public interface IPipelineStep
    {
        float Progress { get; }
        event Action<float> OnProgressChanged;

        IEnumerator Run(Action<IStepResult> result);
    }

    public interface IStepResult
    {
        public struct Success : IStepResult { }
        public struct Failed : IStepResult { }
    }

    /// <summary>
    /// A base class for Monobehaviour steps.
    /// </summary>
    public abstract class AbstractStepBehaviour : MonoBehaviour, IPipelineStep
    {
        public Pipeliner Pipeliner { get; set; }
        public float Progress { get; protected set; }
        public event Action<float> OnProgressChanged;

        public virtual void Setup(Pipeliner pipeliner)
        {
            Pipeliner = pipeliner;
        }
        
        public virtual IEnumerator Run(Action<IStepResult> result)
        {
            SetProgress(0f);
            
            result?.Invoke(new IStepResult.Success());
            return null;
        }

        protected virtual void SetProgress(float value, bool notify = true)
        {
            Progress = value;
            if(notify)
                OnProgressChanged?.Invoke(Progress);
        }
    }
}