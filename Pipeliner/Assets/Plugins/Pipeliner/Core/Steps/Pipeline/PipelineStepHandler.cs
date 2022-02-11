using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a Pipeline from a MonoBehaviour or Scriptable Object.
    /// </summary>
    [AddComponentMenu(MENU_PATH + "Pipeline Step")]
    public class PipelineStepHandler : StepHandlerBehaviour
    {
        public Object Pipeline;

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

        public override IStep[] Create(PipelineRunner runner)
        {
            var pipeline = FindPipeline(Pipeline);

            if (pipeline == null)
                return new IStep[]{new AbstractStep(runner, default)};

            var steps = new List<IStep> { new PipelineStep(runner, new PipelineStepParameters {Pipeline = pipeline}) };
            steps.AddRange(pipeline.Create(runner));
            return steps.ToArray();
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