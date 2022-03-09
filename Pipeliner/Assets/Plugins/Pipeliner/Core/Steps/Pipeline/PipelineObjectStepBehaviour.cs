using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a Pipeline from a Pipeline Object.
    /// </summary>
    [AddComponentMenu(MENU_PATH + "Pipeline Object Step")]
    public class PipelineObjectStepBehaviour : StepFactoryBehaviour
    {
        public PipelineObject Pipeline;
        
        public override IStep[] Create()
        {
            var pipeline = Pipeline.Create();
            var steps = new List<IStep> { new PipelineStep(new PipelineStepParameters {Pipeline = pipeline}) };
            steps.AddRange(pipeline.Steps);
            return steps.ToArray();
        }
    }
}