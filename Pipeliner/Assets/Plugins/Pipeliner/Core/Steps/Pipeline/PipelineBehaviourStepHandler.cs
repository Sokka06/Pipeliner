using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a Pipeline from a Pipeline Behaviour.
    /// </summary>
    [AddComponentMenu(MENU_PATH + "Pipeline Behaviour Step")]
    public class PipelineBehaviourStepHandler : StepHandlerBehaviour
    {
        public PipelineBehaviour Pipeline;
        
        public override IStep[] Create(PipelineRunner runner)
        {
            var steps = new List<IStep> { new PipelineStep(runner, new PipelineStepParameters {Pipeline = Pipeline}) };
            steps.AddRange(Pipeline.Create(runner));
            return steps.ToArray();
        }
    }
}