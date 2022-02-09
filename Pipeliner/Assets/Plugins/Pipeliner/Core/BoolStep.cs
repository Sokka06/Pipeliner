using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public class BoolStep : AbstractStepBehaviour
    {
        public bool Boolean;
        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return base.Run(result);
            
            if (!Boolean)
            {
                result?.Invoke(new IStepResult.Failed());
            }
            
            SetProgress(1f);
        }
    }
}