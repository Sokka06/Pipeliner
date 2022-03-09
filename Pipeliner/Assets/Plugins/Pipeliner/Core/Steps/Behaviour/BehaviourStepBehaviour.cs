using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a Step from given Behaviour.
    /// </summary>
    [AddComponentMenu(MENU_PATH + "Behaviour Step")]
    public class BehaviourStepBehaviour : StepFactoryBehaviour
    {
        public StepFactoryBehaviour StepBehaviour;
        
        public override IStep[] Create()
        {
            var steps = new List<IStep> { new BehaviourStep(default) };
            steps.AddRange(StepBehaviour.Create());
            return steps.ToArray();
        }
    }
}