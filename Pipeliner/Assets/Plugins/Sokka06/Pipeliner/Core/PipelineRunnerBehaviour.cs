using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a Pipeline from MonoBehaviour or Scriptable Object using Pipeline Runner.
    /// </summary>
    [AddComponentMenu("Pipeliner/Pipeline Runner")]
    public class PipelineRunnerBehaviour : MonoBehaviour
    {
        [Tooltip("Run Pipeline on Start.")]
        public bool AutoRun;
        [Tooltip("A GameObject containing a Pipeline Behaviour or a Pipeline Scriptable Object")]
        public Object Pipeline;
        
        [Space]
        public PipelineRunnerSettings Settings;

        public PipelineRunner Runner { get; protected set; }
        public List<IPipelineResult> Results { get; protected set; } = new List<IPipelineResult>();

        public event Action<IPipelineResult> onPipelineFinished;

        protected virtual void OnValidate()
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

        protected virtual void Start()
        {
            if (AutoRun)
                RunPipeline();
        }
        
        /// <summary>
        /// Runs Pipeline from GameObject/Scriptable Object.
        /// </summary>
        /// <param name="result"></param>
        public virtual void RunPipeline(Action<IPipelineResult> result = default)
        {
            StartCoroutine(Run(result));
        }
        
        public virtual IEnumerator Run(Action<IPipelineResult> result = default)
        {
            var pipeline = Utils.FindPipeline(Pipeline)?.Create();

            if (pipeline != null)
            {
                Runner = new PipelineRunner(pipeline, Settings);
                yield return Runner.Run(value =>
                {
                    result?.Invoke(value);
                    Results.Add(value);
                    onPipelineFinished?.Invoke(value);
                });
            }
        }
    }
}