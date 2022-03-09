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
        IStep[] Steps { get; }
        
        /// <summary>
        /// Gets first Step for given type.
        /// </summary>
        /// <typeparam name="T">Step type</typeparam>
        /// <returns></returns>
        T GetStep<T>() where T : AbstractStep;
    }
}