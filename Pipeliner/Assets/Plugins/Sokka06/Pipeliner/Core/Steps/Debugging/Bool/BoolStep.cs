using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    [Serializable]
    public struct BoolStepParameters : IStepParameters
    {
        public bool Boolean;
    }
    
    /// <summary>
    /// A step used for debugging which fails the step if Boolean is set to false.
    /// </summary>
    public class BoolStep : AbstractStep
    {
        public BoolStep(BoolStepParameters parameters) : base(parameters)
        {
        }
        
        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return null;
            var parameters = (BoolStepParameters)Parameters;

            var stepResult = parameters.Boolean ? new IStepResult.Success() as IStepResult : new IStepResult.Failed() as IStepResult;

            Progress = 1f;
            result?.Invoke(stepResult);
        }
    }
}