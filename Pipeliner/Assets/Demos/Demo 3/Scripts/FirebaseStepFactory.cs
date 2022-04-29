using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;

public class FirebaseStepFactory : StepFactoryBehaviour
{
    public override IStep[] Create()
    {
        return new IStep[]{new FirebaseStep(default)};
    }
}
