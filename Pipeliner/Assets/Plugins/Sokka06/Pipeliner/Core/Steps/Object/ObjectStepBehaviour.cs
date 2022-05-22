using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a Step from given Step Object.
    /// </summary>
    [AddComponentMenu(MENU_PATH + "Object Step")]
    public class ObjectStepBehaviour : StepFactoryBehaviour
    {
        public StepFactoryObject StepObject;

        public override IStep[] Create()
        {
            var steps = new List<IStep> { new ObjectStep(default) };
            steps.AddRange(StepObject.Create());
            return steps.ToArray();
        }
    }
}