using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sokka06.Pipeliner;
using UnityEditor;

public static class Utils
{
    /// <summary>
    /// Instantiates runner from a prefab.
    /// </summary>
    /// <returns></returns>
    public static PipelineRunnerBehaviour InstantiateRunner()
    {
        var prefab = AssetDatabase.LoadAssetAtPath("Assets/Tests/PlayMode/Runner.prefab", typeof(PipelineRunnerBehaviour)) as PipelineRunnerBehaviour;
        return Object.Instantiate(prefab);
    }
}
