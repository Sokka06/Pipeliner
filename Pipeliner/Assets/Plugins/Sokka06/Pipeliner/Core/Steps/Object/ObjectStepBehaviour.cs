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

        public override IStep[] Create()
        {
            var steps = new List<IStep> { new ObjectStep(new ObjectStepParameters()) };
            steps.AddRange(StepObject.Create());
            return steps.ToArray();
        }
    }
}