using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Sokka06.Pipeliner;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class RunnerTests
{
    [Test]
    public void _01Run()
    {
        var runner = Utils.InstantiateRunner();
        var pipeline = InstantiatePipeline();

        runner.Pipeline = pipeline;
        runner.Run(Assert.IsInstanceOf<IPipelineResult.Success>);
    }
    
    [Test]
    public void _02Abort()
    {
        var runner = Utils.InstantiateRunner();
        var pipeline = InstantiatePipeline();

        runner.Pipeline = pipeline;
        runner.Run(Assert.IsInstanceOf<IPipelineResult.Success>);
        
        /*var result = default(IPipelineResult);
        var e = runner.Run(value => result = value);
        while (e.MoveNext())
        {
            yield return e.Current;
            runner.Runner.Abort();
        }
        
        Assert.IsInstanceOf<IPipelineResult.Aborted>(result);*/
    }
    
    private PipelineBehaviour InstantiatePipeline()
    {
        var prefab = AssetDatabase.LoadAssetAtPath("Assets/Tests/PlayMode/Pipeline.prefab", typeof(PipelineBehaviour)) as PipelineBehaviour;
        return Object.Instantiate(prefab);
    }
}
