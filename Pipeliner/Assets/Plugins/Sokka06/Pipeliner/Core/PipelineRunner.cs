using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public interface IPipelineRunnerState : IState
    {
        public struct Idle : IPipelineRunnerState { }
        public struct Running : IPipelineRunnerState { }
        public struct Finished : IPipelineRunnerState { }
    }

    [Serializable]
    public struct PipelineRunnerSettings
    {
        public bool AbortOnFail;
    }

    /// <summary>
    /// Used to run a Pipeline. Should be used with a MonoBehaviour.
    /// </summary>
    public class PipelineRunner
    {
        public readonly PipelineRunnerSettings Settings;
        public readonly IPipeline Pipeline;
        
        private float _progress;
        private bool _abortRequested;
        
        public StateMachine<IPipelineRunnerState> State { get; private set; }
        public int StepIndex { get; private set; }
        public Logger Logger { get; private set; }
        public IPipelineResult Result { get; private set; }
        
        public float Progress
        {
            get => _progress;
            private set => _progress = Mathf.Clamp01(value);
        }

        public PipelineRunner(IPipeline pipeline, PipelineRunnerSettings settings = default)
        {
            Pipeline = pipeline;
            Settings = settings;
            
            State = new StateMachine<IPipelineRunnerState>(new IPipelineRunnerState.Idle());
            Logger = new Logger();
        }

        public IEnumerator Run(Action<IPipelineResult> pipelineResult)
        {
            Logger.Log($"Running Pipeline...");
            
            State.SetState(new IPipelineRunnerState.Running());
            
            var result = new IPipelineResult.Default() as IPipelineResult;
            var stepResults = new List<(IStep step, IStepResult result)>(Pipeline.Steps.Length);
            
            Progress = 0f;
            
            for (int i = 0; i < Pipeline.Steps.Length; i++)
            {
                StepIndex = i;
                var currentStep = Pipeline.Steps[StepIndex];
                
                Logger.Log($"Running Step: {currentStep.GetType().Name}");
                
                var stepResult = default(IStepResult);
                var e = currentStep.Run(value => stepResult = value);
                while (e.MoveNext())
                {
                    if (_abortRequested)
                    {
                        currentStep.OnAbort();
                        stepResult = new IStepResult.Aborted();
                        break;
                    }
                    
                    yield return e.Current;
                    CalculateTotalProgress();
                }

                Logger.Log($"Finished Step: {currentStep.GetType().Name}, {stepResult.GetType().Name}");
                
                stepResults.Add((currentStep, stepResult));
            }
            
            Progress = 1f;

            if (_abortRequested)
            {
                result = new IPipelineResult.Aborted(stepResults);
                _abortRequested = false;
            }

            if (result is IPipelineResult.Default)
                result = new IPipelineResult.Success(stepResults);

            Result = result;
            State.SetState(new IPipelineRunnerState.Finished());
            pipelineResult?.Invoke(Result);
            
            Logger.Log($"Finished Pipeline, {Result.GetType().Name}");
        }
        
        /// <summary>
        /// Aborts a running Pipeline.
        /// </summary>
        public void Abort()
        {
            if (State.CurrentState is IPipelineRunnerState.Running)
                _abortRequested = true;
        }
        
        /// <summary>
        /// Calculates total runner progress from all Steps.
        /// </summary>
        private void CalculateTotalProgress()
        {
            Progress = 0f;

            if (Pipeline.Steps.Length > 0)
            {
                var invLength = 1f / Pipeline.Steps.Length;
                
                for (int i = 0; i < Pipeline.Steps.Length; i++)
                {
                    Progress += Pipeline.Steps[i].Progress * invLength;
                }
            }
        }
    }
}