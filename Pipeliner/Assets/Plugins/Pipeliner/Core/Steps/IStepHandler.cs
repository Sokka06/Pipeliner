using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;

public interface IStepHandler
{
    // Creates an instance of step.
    IStep[] Create(PipelineRunner runner);
}
