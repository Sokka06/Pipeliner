using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public struct AddressableSceneStepParameters : IStepParameters
{
    public AddressableScene Scene;

    public AddressableSceneStepParameters(AddressableScene scene)
    {
        Scene = scene;
    }
}

/// <summary>
/// Loads Addressable Scene.
/// </summary>
public class AddressableSceneStep : AbstractStep
{
    public AddressableSceneStep(AddressableSceneStepParameters parameters) : base(parameters)
    {
    }

    public override IEnumerator Run(Action<IStepResult> result)
    {
        base.Run(result);

        var parameters = (AddressableSceneStepParameters)Parameters;
        
        if (!parameters.Scene.Asset.RuntimeKeyIsValid())
        {
            Debug.Log("No Addressable Scene set.");
        }
        
        var handle = Addressables.LoadSceneAsync(parameters.Scene.Asset, parameters.Scene.LoadSceneMode);

        while (!handle.IsDone)
        {
            Progress = handle.PercentComplete;
            yield return null;
        }
                    
        if (parameters.Scene.SetActive)
            SceneManager.SetActiveScene(handle.Result.Scene);

        Progress = 1f;
        result?.Invoke(new IStepResult.Success());
    }
}
