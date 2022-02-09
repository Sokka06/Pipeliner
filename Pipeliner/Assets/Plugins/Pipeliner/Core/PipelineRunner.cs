using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public interface IPipelineState : IState
    {
        public struct Idle : IPipelineState { }
        public struct Running : IPipelineState { }
        public struct Done : IPipelineState { }

        /*public struct Failed : IPipelineState { }
        public struct Success : IPipelineState { }
        public struct Aborted : IPipelineState { }*/
    }
    
    public class PipelineRunner
    {
        private float _progress;
        private bool _abortRequested;
        
        public IPipelineStep[] Steps { get; private set; }
        public StateMachine<IPipelineState> State { get; private set; }
        public IPipelineStep CurrentStep { get; private set; }
        public int StepIndex { get; private set; }

        public event Action OnPipelineFinished;
        public event Action OnStepFinished;

        public float Progress
        {
            get => _progress;
            private set => _progress = Mathf.Clamp01(value);
        }

        public PipelineRunner(IPipelineStep[] steps)
        {
            Steps = steps;
            Progress = 0f;
            State = new StateMachine<IPipelineState>(new IPipelineState.Idle());
        }

        public IEnumerator Run(Action<IPipelineResult> pipelineResult)
        {
            State.SetState(new IPipelineState.Running());
            Progress = 0f;

            var stepResults = new List<(IPipelineStep step, IStepResult result)>(Steps.Length);
            
            for (int i = 0; i < Steps.Length; i++)
            {
                var stepResult = default(IStepResult);
                
                StepIndex = i;
                CurrentStep = Steps[StepIndex];
                CurrentStep.OnProgressChanged += CalculateProgress;
                yield return CurrentStep.Run(value => stepResult = value);
                CurrentStep.OnProgressChanged -= CalculateProgress;
                
                stepResults.Add((CurrentStep, stepResult));

                // Abort run if a step fails.
                if (stepResult is IStepResult.Failed)
                {
                    Debug.LogWarning("Failed");
                    pipelineResult?.Invoke(new IPipelineResult.Failed(stepResults));
                    break;
                }
                
                OnStepFinished?.Invoke();
            }

            Progress = 1f;
            
            State.SetState(new IPipelineState.Done());
            pipelineResult?.Invoke(new IPipelineResult.Success(stepResults));
            OnPipelineFinished?.Invoke();
        }

        public void Abort()
        {
            if (_abortRequested || !(State.CurrentState is IPipelineState.Running))
                return;

            _abortRequested = true;
        }
        
        private void CalculateProgress(float asd)
        {
            Progress = 0f;

            if (Steps.Length > 0)
            {
                for (int i = 0; i < Steps.Length; i++)
                {
                    Progress += Steps[i].Progress;
                }
            
                Progress /= Steps.Length;
            }
            
            Debug.Log($"Progress Changed: {Progress}");
            //return Progress;
        }
    }
}

