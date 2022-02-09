using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public class Pipeliner : MonoBehaviour, IPipeline
    {
        [Tooltip("Run Pipeline at Start.")]
        public bool AutoRun;
        [Tooltip("Stop Pipeline if a step fails.")]
        public bool AbortOnFail;

        public List<AbstractStepBehaviour> Steps { get; protected set; }

        public PipelineRunner Runner { get; protected set; }

        private void Awake()
        {
            SetupSteps();
        }

        private IEnumerator Start()
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

        public void SetupSteps()
        {
            Steps = new List<AbstractStepBehaviour>(FindSteps());
            for (int i = 0; i < Steps.Count; i++)
            {
                Steps[i].Setup(this);
            }
            
            // TODO: Experiment with different casts.
            Runner = new PipelineRunner(Steps.ToArray());
        }

        public AbstractStepBehaviour[] FindSteps()
        {
            return GetComponents<AbstractStepBehaviour>();
        }

        public IEnumerator Run(Action<IPipelineResult> result = null)
        {
            yield return Runner.Run(result);
        }

        public IEnumerator Abort()
        {
            return null;
        }
    }
}