using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public class WaitStep : AbstractStepBehaviour
    {
        public float WaitTime = 1f;
        
        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return base.Run(result);

            var waitTime = WaitTime;

            while (waitTime > 0f)
            {
                SetProgress(1f - waitTime / WaitTime);

                waitTime -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            
            SetProgress(1f);
        }
    }
}