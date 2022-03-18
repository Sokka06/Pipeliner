using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a Pipeline from a MonoBehaviour or Scriptable Object.
    /// </summary>
    [AddComponentMenu(MENU_PATH + "Pipeline Step")]
    public class PipelineStepBehaviour : StepFactoryBehaviour
    {
        public Object Pipeline;

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

        public override IStep[] Create()
        {
            var pipelineFactory = Utils.FindPipeline(Pipeline);

            if (pipelineFactory == null)
                return new IStep[]{};

            var pipeline = pipelineFactory.Create();
            var steps = new List<IStep> { new PipelineStep(new PipelineStepParameters {Pipeline = pipeline}) };
            steps.AddRange(pipeline.Steps);
            return steps.ToArray();
        }
        
    }
}