using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    [AddComponentMenu(MENU_PATH + "Debugging/Bool Step")]
    public class BoolStepBehaviour : StepFactoryBehaviour
    {
        [Tooltip("Fails this step if false.")]
        public bool Boolean = true;
    
        public override IStep[] Create(PipelineRunner runner)
        {
            return new IStep[] {new BoolStep(runner, new BoolStepParameters{Boolean = Boolean})};
        }
    }
}