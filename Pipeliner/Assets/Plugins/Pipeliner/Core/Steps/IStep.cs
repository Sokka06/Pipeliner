using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public interface IStepParameters { }
    
    public interface IStep
    {
        IStepParameters Parameters { get; }
        EventValue<float> Progress { get; }

        IEnumerator Run(Action<IStepResult> result);

        void Abort();
    }

    public interface IStepResult
    {
        public struct Default : IStepResult { }
        public struct Success : IStepResult { }
        public struct Failed : IStepResult { }
        public struct Aborted : IStepResult {}
    }
    
    public interface IStepState
    {
        public struct Pending : IStepState { }
        public struct Running : IStepState { }
        public struct Idle : IStepState { }
    }
}