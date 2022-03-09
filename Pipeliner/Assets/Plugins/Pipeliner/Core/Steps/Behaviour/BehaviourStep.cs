using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public class BehaviourStep : AbstractStep
    {
        public BehaviourStep(IStepParameters parameters) : base(parameters)
        {
        }

        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return base.Run(result);
            Progress.Value = 1f;
        }
    }
}