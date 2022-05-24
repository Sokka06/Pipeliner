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
        
        /// <summary>
        /// Pipeline ran successfully.
        /// </summary>
        public struct Success : IPipelineResult
        {
            public (IStep step, IStepResult result)[] StepResults { get; private set; }

            public Success((IStep step, IStepResult result)[] stepResults)
            {
                StepResults = stepResults;
            }
        }

        /// <summary>
        /// Pipeline was aborted.
        /// </summary>
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
    /// Pipeline is a container for all steps.
    /// </summary>
    public interface IPipeline
    {
        /// <summary>
        /// All steps.
        /// </summary>
        IStep[] Steps { get; }
        
        /// <summary>
        /// Gets first instance of given Step type.
        /// </summary>
        /// <typeparam name="T">Step type to get.</typeparam>
        /// <returns>Step of given type.</returns>
        T GetStep<T>() where T : IStep;
        
        /// <summary>
        /// Gets all Steps of given type.
        /// </summary>
        /// <param name="steps">Array that will be filled with matching Steps.</param>
        /// <typeparam name="T">Step type to get.</typeparam>
        /// <returns>Count of found Steps.</returns>
        int GetSteps<T>(ref T[] steps) where T : IStep;

        /// <summary>
        /// Gets index of given Step.
        /// </summary>
        /// <param name="step"></param>
        /// <returns>Index of step, -1 if not found.</returns>
        int GetIndex(IStep step);
    }
}