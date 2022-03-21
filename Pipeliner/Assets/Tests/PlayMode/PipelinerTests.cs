using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Sokka06.Pipeliner;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class PipelinerTests
{
    [Test]
    public void _01Success()
    {
        var runner = Utils.InstantiateRunner();
        var pipeline = InstantiatePipeline();

        runner.Pipeline = pipeline;
        runner.Run(Assert.IsInstanceOf<IPipelineResult.Success>);
    }

    private PipelineBehaviour InstantiatePipeline()
    {
        var prefab = AssetDatabase.LoadAssetAtPath("Assets/Tests/PlayMode/Pipeline.prefab", typeof(PipelineBehaviour)) as PipelineBehaviour;
        return Object.Instantiate(prefab);
    }
}
