using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    [CreateAssetMenu(fileName = "Debug Log Step", menuName = "Pipeliner/Steps/Debug Log")]
    public class DebugLogStepObject : StepFactoryObject
    {
        public DebugLogParameters Parameters;
        
        public override IStep[] Create()
        {
            return new IStep[] {new DebugLogStep(Parameters)};
        }
    }
}