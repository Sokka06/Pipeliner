using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Sokka06.Pipeliner;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class PipelinerTests
{

    [UnityTest]
    public IEnumerator _01Success()
    {
        InstantiateRunnerAndPipeline(out var runner, out var pipelineFactory);

        runner.Pipeline = pipelineFactory;
        
        var result = default(IPipelineResult);
        yield return runner.Run(value => result = value);
        
        Assert.IsInstanceOf<IPipelineResult.Success>(result);
    }
    
    /// <summary>
    /// Instantiates runner from a prefab.
    /// </summary>
    /// <returns></returns>
    private void InstantiateRunnerAndPipeline(out PipelineRunnerBehaviour runner, out PipelineBehaviour pipeline)
    {
        var prefab = AssetDatabase.LoadAssetAtPath("Assets/Tests/PlayMode/Test.prefab", typeof(GameObject)) as GameObject;
        var o = GameObject.Instantiate(prefab);

        runner = o.GetComponentInChildren<PipelineRunnerBehaviour>();
        pipeline = o.GetComponentInChildren<PipelineBehaviour>();
    }
}
