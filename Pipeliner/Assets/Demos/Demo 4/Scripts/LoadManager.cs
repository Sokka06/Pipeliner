using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Demo4;
using Sokka06.Pipeliner;
using UnityEngine;

/// <summary>
/// Loads a Load Profiles using Pipeline Runner.
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

        var pipeline = new Pipeline(steps);
        Runner = new PipelineRunner(pipeline);

        onLoadBegin?.Invoke(profile);

        var result = default(IPipelineResult);
        yield return Runner.Run(value => result = value);

        onLoadFinish?.Invoke(profile);
        
        CurrentProfile = profile;
    }
}
