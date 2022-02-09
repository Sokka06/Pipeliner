using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// A base class for Scriptable Object steps.
    /// </summary>
    public abstract class AbstractStepObject : ScriptableObject, IPipelineStep
    {
        public float Progress { get; protected set; }
        public event Action<float> OnProgressChanged;

        public IEnumerator Run(Action<IStepResult> result)
        {
            //SetProgress(0f);
            
            result?.Invoke(new IStepResult.Success());
            return null;
        }
    }
}