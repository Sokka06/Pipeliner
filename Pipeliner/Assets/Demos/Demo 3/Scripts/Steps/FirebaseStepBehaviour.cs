using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;

namespace Demos.Demo3
{
    public class FirebaseStepBehaviour : StepFactoryBehaviour
    {
        public override IStep[] Create()
        {
            return new IStep[]{new FirebaseStep(default)};
        }
    }
}
