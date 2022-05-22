using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    [Serializable]
    public struct WaitParameters : IStepParameters
    {
        public float WaitTime;

        public WaitParameters(float waitTime)
        {
            WaitTime = waitTime;
        }
    }
    
    /// <summary>
    /// Waits for given time in seconds.
    /// </summary>
    public class WaitStep : AbstractStep
    {
        public WaitStep(IStepParameters parameters) : base(parameters)
        {
        }
        
        public override IEnumerator Run(Action<IStepResult> result)
        {
            var stepResult = new IStepResult.Default() as IStepResult;

            var parameters = (WaitParameters)Parameters;
            
            var waitTime = parameters.WaitTime;

            while (waitTime > 0f)
            {
                waitTime -= Time.deltaTime;
                Progress = 1f - waitTime / parameters.WaitTime;
                yield return null;
            }
            
            Progress = 1f;

            if (stepResult is IStepResult.Default)
                stepResult = new IStepResult.Success();
            
            result?.Invoke(stepResult);
        }
    }
}