using System;
using System.Collections;
using System.Collections.Generic;
using Sokka06.Pipeliner;
using UnityEngine;

namespace Demos.Demo3
{
    public class FirebaseStep : AbstractStep
    {
        public FirebaseStep(IStepParameters parameters) : base(parameters)
        {
        
        }

        public override IEnumerator Run(Action<IStepResult> result)
        {
            FakeFirebase.Instance.Initialize();
            while (!FakeFirebase.Instance.IsInitialized)
            {
                yield return null;
            }
            
            Progress = 1f;
            result?.Invoke(new IStepResult.Success());
        }
    }
}