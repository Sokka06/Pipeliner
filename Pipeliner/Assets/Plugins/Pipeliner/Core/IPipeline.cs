using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public interface IPipelineResult
    {
        List<(IPipelineStep step, IStepResult result)> StepResults { get; }
    
        public struct Success : IPipelineResult
        {
            public List<(IPipelineStep step, IStepResult result)> StepResults { get; private set; }

            public Success(List<(IPipelineStep step, IStepResult result)> stepResults)
            {
                StepResults = stepResults;
            }
        }
        public struct Failed : IPipelineResult
        {
            public List<(IPipelineStep step, IStepResult result)> StepResults { get; private set; }
            
            public Failed(List<(IPipelineStep step, IStepResult result)> stepResults)
            {
                StepResults = stepResults;
            }
        }
    }

    public interface IPipeline
    {
        IEnumerator Run(Action<IPipelineResult> result);
    }
}