using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public interface IPipelineResult
    {
        (IStep step, IStepResult result)[] StepResults { get; }

        public struct Default : IPipelineResult
        {
            public (IStep step, IStepResult result)[] StepResults { get; }
        }
        
        public struct Success : IPipelineResult
        {
            public (IStep step, IStepResult result)[] StepResults { get; private set; }

            public Success((IStep step, IStepResult result)[] stepResults)
            {
                StepResults = stepResults;
            }
        }
        
        public struct Failed : IPipelineResult
        {
            public (IStep step, IStepResult result)[] StepResults { get; private set; }
            
            public Failed((IStep step, IStepResult result)[] stepResults)
            {
                StepResults = stepResults;
            }
        }
        
        public struct Aborted : IPipelineResult
        {
            public (IStep step, IStepResult result)[] StepResults { get; private set; }
            
            public Aborted((IStep step, IStepResult result)[] stepResults)
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
        IStep[] Steps { get; }
        
        /// <summary>
        /// Gets first Step for given type.
        /// </summary>
        /// <typeparam name="T">Step type</typeparam>
        /// <returns></returns>
        T GetStep<T>() where T : AbstractStep;
        
        /// <summary>
        /// Gets all Steps of given type.
        /// </summary>
        /// <param name="steps">Array that will be filled with matching Steps.</param>
        /// <typeparam name="T">Step type to get.</typeparam>
        /// <returns>Count of found Steps.</returns>
        int GetSteps<T>(ref T[] steps) where T : AbstractStep;
    }
}