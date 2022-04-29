using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Loads fake login data to simulate Google Play Services.
/// </summary>
public class PlayServicesStep : AbstractStep
{
    
    public PlayServicesStep(IStepParameters parameters) : base(parameters)
    {
        
    }

    public override IEnumerator Run(Action<IStepResult> result)
    {
        base.Run(result);
        
        FakePlayServices.Instance.Initialize();

        while (!FakePlayServices.Instance.IsInitialized)
        {
            yield return null;
        }

        Progress = 1f;
        result?.Invoke(new IStepResult.Success());
    }
}
