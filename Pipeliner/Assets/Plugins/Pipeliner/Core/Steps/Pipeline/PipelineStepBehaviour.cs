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

        public override IStep[] Create(PipelineRunner runner)
        {
            var pipeline = Utils.FindPipeline(Pipeline);

            if (pipeline == null)
                return new IStep[]{new AbstractStep(runner, default)};

            var steps = new List<IStep> { new PipelineStep(runner, new PipelineStepParameters {Pipeline = pipeline}) };
            steps.AddRange(pipeline.Create(runner));
            return steps.ToArray();
        }
        
    }
}