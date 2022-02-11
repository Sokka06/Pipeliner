using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a Pipeline from a Pipeline Object.
    /// </summary>
    [AddComponentMenu(MENU_PATH + "Pipeline Object Step")]
    public class PipelineObjectStepHandler : StepHandlerBehaviour
    {
        public PipelineObject Pipeline;
        
        public override IStep[] Create(PipelineRunner runner)
        {
            var steps = new List<IStep> { new PipelineStep(runner, new PipelineStepParameters {Pipeline = Pipeline}) };
            steps.AddRange(Pipeline.Create(runner));
            return steps.ToArray();
        }
    }
}