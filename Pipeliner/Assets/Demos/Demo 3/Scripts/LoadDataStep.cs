using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Common;
using Sokka06.Pipeliner;
using UnityEngine;

public class LoadDataStep<T> : AbstractStep where T : IData
{
    public LoadDataStep(LoadDataStepParameters parameters) : base(parameters)
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

        yield return parameters.DataSystem.Load((T)parameters.Data);
        result?.Invoke(new IStepResult.Success());
    }
}
