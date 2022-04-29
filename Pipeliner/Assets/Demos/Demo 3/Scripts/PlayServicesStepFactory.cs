using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;

public class PlayServicesStepFactory : StepFactoryBehaviour
{
    public override IStep[] Create()
    {
        return new IStep[]{new PlayServicesStep(default)};
    }
}
