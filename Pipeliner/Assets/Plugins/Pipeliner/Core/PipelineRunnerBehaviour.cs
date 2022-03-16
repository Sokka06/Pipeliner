using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sokka06.Pipeliner
{
    [AddComponentMenu("Pipeliner/Pipeline Runner")]
    public class PipelineRunnerBehaviour : MonoBehaviour
    {
        [Tooltip("A GameObject containing a Pipeline Behaviour or a Pipeline Scriptable Object")]
        public Object Pipeline;
        [Tooltip("Run Pipeline at Start.")]
        public bool AutoRun;
        
        [Space]
        public PipelineRunnerSettings Settings;

        public PipelineRunner Runner { get; private set; }
        
        public event Action OnPipelineFinished;
        public event Action OnStepFinished;

        private void OnValidate()
        {
            if (Pipeline != null)
            {
                var pipeline = Utils.FindPipeline(Pipeline);

                if (pipeline == null)
                {
                    Debug.LogWarning($"No Pipeline found from {Pipeline.name}.");
                    Pipeline = null;
                }
            }
        }

        private void Awake()
        {
            
        }

        protected virtual IEnumerator Start()
        {
            if (AutoRun)
            {
                var pipeline = Utils.FindPipeline(Pipeline)?.Create();
                var result = default(IPipelineResult);
                yield return Run(pipeline, value => result = value);
                
                /*foreach (var VARIABLE in result.StepResults)
                {
                    Debug.Log(VARIABLE);
                }*/
            }
        }
        
        /// <summary>
        /// Runs given Pipeline.
        /// </summary>
        /// <param name="pipeline"></param>
        /// <param name="result"></param>
        public virtual IEnumerator Run(IPipeline pipeline, Action<IPipelineResult> result = default)
        {
            Runner = new PipelineRunner(pipeline, Settings);
            yield return Runner.Run(result);
        }
    }
}

