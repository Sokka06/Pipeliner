using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Sokka06.Pipeliner;
using UnityEngine;

public class PipelineTests
{
    [Test]
    public void _01Created()
    {
        var steps = new IStep[]
        {
            new DebugLogStep(default),
            new DebugLogStep(default),
            new DebugLogStep(default),
        };
        var pipeline = new Pipeline(steps);
        
        Debug.Log($"Steps: {pipeline.Steps.Length}");
        Assert.Greater(pipeline.Steps.Length, 0);
    }
    
    [Test]
    public void _02GetStep()
    {
        var steps = new IStep[]
        {
            new WaitStep(default),
            new DebugLogStep(default),
            new WaitStep(default),
        };
        var pipeline = new Pipeline(steps);
        var step = pipeline.GetStep<DebugLogStep>();
        
        Debug.Log(step.GetType().Name);
        Assert.IsInstanceOf<DebugLogStep>(step);
    }
    
    [Test]
    public void _03GetSteps()
    {
        var steps = new IStep[]
        {
            new WaitStep(default),
            new DebugLogStep(default),
            new WaitStep(default),
        };
        var pipeline = new Pipeline(steps);

        var waitSteps = new WaitStep[2];
        var count = pipeline.GetSteps(ref waitSteps);
        Assert.AreEqual(2,count);

        for (int i = 0; i < count; i++)
        {
            Debug.Log($"{i}: {waitSteps[i].GetType().Name}");
            Assert.IsInstanceOf<WaitStep>(waitSteps[i]);
        }
    }
}
