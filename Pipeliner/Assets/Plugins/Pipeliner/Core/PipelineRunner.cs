using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

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

    public class PipelineRunnerData
    {
        public IPipeline Pipeline;
        public IStep[] Steps;
        public int StepIndex;
    }

    [AddComponentMenu("Pipeliner/Pipeline Runner")]
    public class PipelineRunner : MonoBehaviour
    {
        [Tooltip("A GameObject containing a Pipeline Behaviour or a Pipeline Object")]
        public Object Pipeline;
        
        [Space]
        [Tooltip("Run Pipeline at Start.")]
        public bool AutoRun;
        [Tooltip("Stop a running Pipeline if a Step fails.")]
        public bool AbortOnFail = true;
        
        private float _progress;
        private bool _abortRequested;
        
        public StateMachine<IPipelineState> State { get; private set; }
        public PipelineRunnerData Data { get; private set; }
        
        public IStep[] Steps { get; private set; }
        public IStep CurrentStep { get; private set; }
        public int StepIndex { get; private set; }
        
        public Logger Logger { get; private set; }

        public event Action OnPipelineFinished;
        public event Action OnStepFinished;

        public float Progress
        {
            get => _progress;
            private set => _progress = Mathf.Clamp01(value);
        }
        
        private void OnValidate()
        {
            if (Pipeline != null)
            {
                var pipeline = FindPipeline(Pipeline);

                if (pipeline == null)
                {
                    Debug.LogWarning($"No Pipeline found from {Pipeline.name}.");
                    Pipeline = null;
                }
            }
        }

        private void Awake()
        {
            var pipeline = FindPipeline(Pipeline);
            Steps = pipeline.Create(this);
            
            Progress = 0f;
            State = new StateMachine<IPipelineState>(new IPipelineState.Idle());
            Data = new PipelineRunnerData();
            Logger = new Logger();
            Logger.Log("Initialized Pipeline Runner");
        }

        protected virtual IEnumerator Start()
        {
            if (AutoRun)
            {
                var result = default(IPipelineResult);
                yield return Run(value => result = value);
                foreach (var VARIABLE in result.StepResults)
                {
                    Debug.Log(VARIABLE);
                }
            }
        }

        public virtual IEnumerator Run(Action<IPipelineResult> pipelineResult)
        {
            Logger.Log($"Running Pipeline: {Pipeline.name}...");
            
            State.SetState(new IPipelineState.Running());
            Progress = 0f;
            
            var result = new IPipelineResult.Default() as IPipelineResult;
            var stepResults = new List<(IStep step, IStepResult result)>(Steps.Length);
            
            for (int i = 0; i < Steps.Length; i++)
            {
                var stepResult = default(IStepResult);
                
                StepIndex = i;
                CurrentStep = Steps[StepIndex];
                
                Logger.Log($"Running Step: {CurrentStep.GetType().Name}");
                
                CurrentStep.Progress.OnValueChanged += CalculateProgress;
                yield return CurrentStep.Run(value => stepResult = value);
                CurrentStep.Progress.OnValueChanged -= CalculateProgress;
                
                //OnStepFinished?.Invoke();
                
                Logger.Log($"Finished Step: {CurrentStep.GetType().Name}, {stepResult.GetType().Name}");
                
                stepResults.Add((CurrentStep, stepResult));

                // Abort run if a step fails.
                if (AbortOnFail && stepResult is IStepResult.Failed)
                {
                    result = new IPipelineResult.Aborted(stepResults);
                    break;
                }

                if (_abortRequested)
                {
                    result = new IPipelineResult.Aborted(stepResults);
                    _abortRequested = false;
                    break;
                }
            }

            if (result is IPipelineResult.Default)
                result = new IPipelineResult.Success(stepResults);

            Progress = 1f;
            
            State.SetState(new IPipelineState.Done());
            pipelineResult?.Invoke(result);
            OnPipelineFinished?.Invoke();
            
            Logger.Log($"Finished Pipeline: {Pipeline.name}, {result.GetType().Name}");
        }

        /// <summary>
        /// Aborts a running Pipeline.
        /// </summary>
        public void Abort()
        {
            if (_abortRequested || !(State.CurrentState is IPipelineState.Running))
                return;

            CurrentStep.Abort();
            _abortRequested = true;
        }
        
        private void CalculateProgress(float asd)
        {
            Progress = 0f;

            if (Steps.Length > 0)
            {
                for (int i = 0; i < Steps.Length; i++)
                {
                    Progress += Steps[i].Progress.Value;
                }
            
                Progress /= Steps.Length;
            }
            
            //Debug.Log($"Progress Changed: {Progress}");
            //return Progress;
        }
        
        /// <summary>
        /// Tries to find Pipeline from given object.
        /// </summary>
        /// <returns></returns>
        private IPipeline FindPipeline(Object o)
        {
            // Try to cast Scriptable Object to Pipeline.
            var pipeline = Pipeline as IPipeline;
            
            // Cast failed, try to find Pipeline from a GameObject.
            if (pipeline == null)
            {
                if (Pipeline is GameObject go)
                {
                    pipeline = go.GetComponent<IPipeline>();
                }
            }

            return pipeline;
        }
    }
}

