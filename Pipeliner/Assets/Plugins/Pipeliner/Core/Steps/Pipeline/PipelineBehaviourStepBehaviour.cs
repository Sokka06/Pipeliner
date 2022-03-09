using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a Pipeline from a Pipeline Behaviour.
    /// </summary>
    [AddComponentMenu(MENU_PATH + "Pipeline Behaviour Step")]
    public class PipelineBehaviourStepBehaviour : StepFactoryBehaviour
    {
        public PipelineBehaviour Pipeline;
        
        public override IStep[] Create()
        {
            var pipeline = Pipeline.Create();
            var steps = new List<IStep> { new PipelineStep(new PipelineStepParameters {Pipeline = pipeline}) };
            steps.AddRange(pipeline.Steps);
            return steps.ToArray();
        }
    }
}