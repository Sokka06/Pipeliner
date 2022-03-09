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
            yield return base.Run(result);

            var stepResult = new IStepResult.Default() as IStepResult;

            var parameters = (WaitParameters)Parameters;
            
            var waitTime = parameters.WaitTime;

            while (waitTime > 0f)
            {
                Progress.Value = 1f - waitTime / parameters.WaitTime;

                waitTime -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();

                if (!_abortRequested)
                    continue;
                
                stepResult = new IStepResult.Aborted();
                _abortRequested = false;
                break;
            }

            if (stepResult is IStepResult.Default)
                stepResult = new IStepResult.Success();
            
            result?.Invoke(stepResult);
            Progress.Value = 1f;
        }
    }
}