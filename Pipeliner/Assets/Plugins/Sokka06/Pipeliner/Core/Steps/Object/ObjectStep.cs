using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public struct ObjectStepParameters : IStepParameters
    {
        //public IStep[] Step;
    }
    
    public class ObjectStep : AbstractStep
    {
        public ObjectStep(IStepParameters parameters) : base(parameters)
        {
        }

        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return null;
            Progress = 1f;
            result?.Invoke(new IStepResult.Success());
        }
    }
}