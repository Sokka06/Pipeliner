using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Sokka06.Pipeliner;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class RunnerTests
{
    [UnityTest]
    public IEnumerator _01Run()
    {
        var steps = new IStep[]
        {
            new DebugLogStep(new DebugLogParameters{Message = ""}),
            new DebugLogStep(new DebugLogParameters{Message = ""}),
            new DebugLogStep(new DebugLogParameters{Message = ""}),
        };
        var pipeline = new Pipeline(steps);
        var runner = new PipelineRunner(pipeline);
        
        var result = default(IPipelineResult);
        yield return runner.Run(value => result = value);
        
        Debug.Log(runner.Logger);

        Assert.IsInstanceOf<IPipelineResult.Success>(result);
    }
    
    [UnityTest]
    public IEnumerator _02Abort()
    {
        var steps = new IStep[]
        {
            new DebugLogStep(new DebugLogParameters{Message = ""}),
            new DebugLogStep(new DebugLogParameters{Message = ""}),
            new DebugLogStep(new DebugLogParameters{Message = ""}),
        };
        var pipeline = new Pipeline(steps);
        var runner = new PipelineRunner(pipeline);

        var result = default(IPipelineResult);
        var e = runner.Run(value => result = value);
        while (e.MoveNext())
        {
            yield return e.Current;
            runner.Abort();
        }
        
        Debug.Log(runner.Logger);
        
        Assert.IsInstanceOf<IPipelineResult.Aborted>(result);
    }
    
    [UnityTest]
    public IEnumerator _03AbortOnFail()
    {
        var steps = new IStep[]
        {
            new BoolStep(new BoolStepParameters{Boolean = true}),
            new BoolStep(new BoolStepParameters{Boolean = true}),
            new BoolStep(new BoolStepParameters{Boolean = false}),
            new BoolStep(new BoolStepParameters{Boolean = true}),
            new BoolStep(new BoolStepParameters{Boolean = true})
        };
        var pipeline = new Pipeline(steps);
        var runner = new PipelineRunner(pipeline, new PipelineRunnerSettings{AbortOnFail = true});

        var result = default(IPipelineResult);
        yield return runner.Run(value => result = value);

        Debug.Log(runner.Logger);

        Assert.IsInstanceOf<IPipelineResult.Aborted>(result);
    }
}
