using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a step from given Scriptable Object.
    /// </summary>
    [AddComponentMenu(MENU_PATH + "Object Step")]
    public class ObjectStepBehaviour : StepFactoryBehaviour
    {
        public StepFactoryObject StepObject;

        public override IStep[] Create(PipelineRunner runner)
        {
            var steps = new List<IStep> { new ObjectStep(runner, new ObjectStepParameters()) };
            steps.AddRange(StepObject.Create(runner));
            return steps.ToArray();
        }
    }
}