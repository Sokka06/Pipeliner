using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public interface IStepResult
    {
        public struct Default : IStepResult { }
        /// <summary>
        /// Step ran successfully.
        /// </summary>
        public struct Success : IStepResult { }
        /// <summary>
        /// Step failed while running.
        /// </summary>
        public struct Failed : IStepResult { }
        /// <summary>
        /// Step was aborted while running.
        /// </summary>
        public struct Aborted : IStepResult { }
    }
    
    /*public interface IStepState : IState
    {
        public struct Pending : IStepState { }
        public struct Running : IStepState { }
        public struct Idle : IStepState { }
    }*/
    
    public interface IStepParameters { }
    
    public interface IStep
    {
        IStepParameters Parameters { get; }
        /// <summary>
        /// Pipeline this step is attached to.
        /// </summary>
        IPipeline Pipeline { get; set; }
        /// <summary>
        /// Step progress.
        /// </summary>
        float Progress { get; }

        /// <summary>
        /// Run step.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        IEnumerator Run(Action<IStepResult> result);

        /// <summary>
        /// Called when Step is aborted while running.
        /// </summary>
        void OnAbort();
    }
}