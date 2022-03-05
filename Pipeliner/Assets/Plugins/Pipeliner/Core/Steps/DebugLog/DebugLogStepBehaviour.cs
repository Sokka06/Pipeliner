using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    [AddComponentMenu(MENU_PATH + "Debugging/Debug Log Step")]
    public class DebugLogStepBehaviour : StepFactoryBehaviour
    {
        public DebugLogParameters Parameters;
    
        public override IStep[] Create(PipelineRunner runner)
        {
            return new IStep[] {new DebugLogStep(runner, Parameters)};
        }
    }
}