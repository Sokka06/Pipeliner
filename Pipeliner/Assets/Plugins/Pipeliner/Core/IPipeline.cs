using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public interface IPipelineResult
    {
        List<(IStep step, IStepResult result)> StepResults { get; }

        public struct Default : IPipelineResult
        {
            public List<(IStep step, IStepResult result)> StepResults { get; }
        }
        
        public struct Success : IPipelineResult
        {
            public List<(IStep step, IStepResult result)> StepResults { get; private set; }

            public Success(List<(IStep step, IStepResult result)> stepResults)
            {
                StepResults = stepResults;
            }
        }
        
        public struct Failed : IPipelineResult
        {
            public List<(IStep step, IStepResult result)> StepResults { get; private set; }
            
            public Failed(List<(IStep step, IStepResult result)> stepResults)
            {
                StepResults = stepResults;
            }
        }
        
        public struct Aborted : IPipelineResult
        {
            public List<(IStep step, IStepResult result)> StepResults { get; private set; }
            
            public Aborted(List<(IStep step, IStepResult result)> stepResults)
            {
                StepResults = stepResults;
            }
        }
    }

    /// <summary>
    /// A pipeline holds all steps...
    /// </summary>
    public interface IPipeline
    {
        /// <summary>
        /// Creates a Pipeline from steps.
        /// </summary>
        /// <returns></returns>
        IStep[] Create(PipelineRunner runner);
        
        /*/// <summary>
        /// Runs Pipeline's steps.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        IEnumerator Run(Action<IPipelineResult> result);*/
    }
}