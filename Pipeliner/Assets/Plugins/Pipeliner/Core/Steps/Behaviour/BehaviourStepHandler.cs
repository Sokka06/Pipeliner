using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a Step from given Behaviour.
    /// </summary>
    [AddComponentMenu(MENU_PATH + "Behaviour Step")]
    public class BehaviourStepHandler : StepHandlerBehaviour
    {
        public StepHandlerBehaviour StepBehaviour;
        
        public override IStep[] Create(PipelineRunner runner)
        {
            var steps = new List<IStep> { new BehaviourStep(runner, default) };
            steps.AddRange(StepBehaviour.Create(runner));
            return steps.ToArray();
        }
    }
}