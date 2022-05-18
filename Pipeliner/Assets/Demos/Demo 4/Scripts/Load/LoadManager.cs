using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Demo4;
using Sokka06.Pipeliner;
using UnityEngine;

/// <summary>
/// Loads a Load Profiles using Pipeline Runner.
/// Note: There's currently no way to load multiple scenes and activate them on the same frame in Unity.
/// This means GameObjects in Scene A will run Awake, Start and a couple frames of Update
/// before GameObjects in Scene B start running.
/// </summary>
public class LoadManager : SingletonBehaviour<LoadManager>
{
    public LoadProfile CurrentProfile { get; private set; }
    public PipelineRunner Runner { get; private set; }

    public event Action<LoadProfile> onLoadBegin;
    public event Action<LoadProfile> onLoadFinish;

    public void Load(LoadProfile profile)
    {
        StartCoroutine(StartLoad(profile));
    }

    private IEnumerator StartLoad(LoadProfile profile)
    {
        var steps = new IStep[]
        {
            new WaitStep(new WaitParameters(0.5f)),
            new LoadProfileStep(new LoadProfileStepParameters(profile)),
            new WaitStep(new WaitParameters(0.5f))
        };

        Runner = new PipelineRunner(new Pipeline(steps));

        onLoadBegin?.Invoke(profile);

        var result = default(IPipelineResult);
        yield return Runner.Run(value => result = value);

        onLoadFinish?.Invoke(profile);
        
        CurrentProfile = profile;
    }
}
