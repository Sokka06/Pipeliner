using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sokka06.Pipeliner
{
    [Serializable]
    public struct PipelineStepParameters : IStepParameters
    {
        public IPipeline Pipeline;
    }
    
    /// <summary>
    /// Runs a Pipeline.
    /// </summary>
    public class PipelineStep : AbstractStep
    {
        public PipelineStep(IStepParameters parameters) : base(parameters)
        {
        }

        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return base.Run(result);

            var stepResult = new IStepResult.Default() as IStepResult;

            var parameters = (PipelineStepParameters)Parameters;
            
            if (parameters.Pipeline == null)
                stepResult = new IStepResult.Failed();

            if (stepResult is IStepResult.Default)
                stepResult = new IStepResult.Success();
            
            Progress = 1f;
            result?.Invoke(stepResult);
        }
    }
}