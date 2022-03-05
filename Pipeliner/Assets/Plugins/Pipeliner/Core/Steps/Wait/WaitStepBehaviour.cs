using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    [AddComponentMenu(MENU_PATH + "Wait Step")]
    public class WaitStepBehaviour : StepFactoryBehaviour
    {
        public float WaitTime = 1f;
    
        public override IStep[] Create(PipelineRunner runner)
        {
            return new IStep[] {new WaitStep(runner, new WaitParameters{WaitTime = WaitTime})};
        }
    }
}