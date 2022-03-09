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
        public BoolStep(IStepParameters parameters) : base(parameters)
        {
        }
        
        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return base.Run(result);

            var parameters = (BoolStepParameters)Parameters;
            
            if (!parameters.Boolean)
            {
                result?.Invoke(new IStepResult.Failed());
            }
            
            Progress.Value = 1f;
        }
    }
}