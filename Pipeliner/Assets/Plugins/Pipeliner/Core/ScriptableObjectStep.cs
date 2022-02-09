using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a step from given Scriptable Object
    /// </summary>
    public class ScriptableObjectStep : AbstractStepBehaviour
    {
        public AbstractStepObject ScriptableObject;

        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return base.Run(result);
            
            if (ScriptableObject == null)
            {
                result?.Invoke(new IStepResult.Failed());
            }
            
            yield return ScriptableObject.Run(result);
        }
    }
}