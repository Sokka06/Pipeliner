using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    [CreateAssetMenu(fileName = "Wait Step", menuName = MENU_PATH + "Wait")]
    public class WaitStepObject : StepFactoryObject
    {
        public float WaitTime = 1f;
        
        public override IStep[] Create(PipelineRunner runner)
        {
            return new IStep[] {new WaitStep(runner, new WaitParameters { WaitTime = WaitTime })};
        }
    }
}