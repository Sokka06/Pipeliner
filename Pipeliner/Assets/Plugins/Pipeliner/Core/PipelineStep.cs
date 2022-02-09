using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// Runs a Pipeline.
    /// </summary>
    public class PipelineStep : AbstractStepBehaviour
    {
        /*[SerializeReference]
        public Object Pipeline;*/
        public Pipeliner Pipeline;

        private void OnValidate()
        {
            return;
            if (Pipeline != null)
            {
                var type = Pipeline.GetType();
                
                // Try to find IPipeline from MonoBehaviour.
                if (!(Pipeline is IPipeline) && Pipeline is MonoBehaviour monoBehaviour)
                {
                    Pipeline = monoBehaviour.GetComponent<IPipeline>() as Pipeliner;
                }

                if (Pipeline == null)
                {
                    Debug.LogWarning($"{type} doesn't implement IPipeline interface.");
                    //Pipeline = null;
                }
            }
        }

        public override IEnumerator Run(Action<IStepResult> result)
        {
            yield return base.Run(result);
            
            if (!(Pipeline is IPipeline pipeline))
            {
                result?.Invoke(new IStepResult.Failed());
            }
            else
            {
                var pipelineResult = default(IPipelineResult);
                yield return pipeline.Run(value => pipelineResult = value);

                if (pipelineResult is IPipelineResult.Failed)
                {
                    Debug.LogWarning("Pipeline Step failed because a step in Pipeline failed.");
                    result?.Invoke(new IStepResult.Failed());
                }
            }
        }
    }
}