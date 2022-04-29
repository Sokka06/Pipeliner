using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Common;
using Sokka06.Pipeliner;
using UnityEngine;

public class SocialServicesStep : AbstractStep
{
    public SocialServicesStep(IStepParameters parameters) : base(parameters)
    {
        
    }

    public override IEnumerator Run(Action<IStepResult> result)
    {
        var parameters = (LoadDataStepParameters)Parameters;

        // Wait a second, because loading a small file from disk takes less than one frame.
        var waitTime = 1f;
        while (waitTime > 0f)
        {
            waitTime -= Time.deltaTime;
            Progress = 1f - waitTime / 1f;
            yield return null;
        }

        
        result?.Invoke(new IStepResult.Success());
    }
}