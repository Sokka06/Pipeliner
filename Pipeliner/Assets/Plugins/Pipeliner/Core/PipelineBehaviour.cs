using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// A MonoBehaviour based Pipeline. Steps should be added on the same GameObject as this script.
    /// </summary>
    [AddComponentMenu("Pipeliner/Pipeline")]
    public class PipelineBehaviour : MonoBehaviour, IPipeline
    {
        public List<StepHandlerBehaviour> Steps;

        /*protected virtual void Awake()
        {
            SetupSteps();
        }

        public virtual void SetupSteps()
        {
            //Steps = new List<IStepHandler>(FindSteps());
            for (int i = 0; i < Steps.Count; i++)
            {
                //Steps[i].Setup(this);
            }
        }*/
        
        public virtual StepHandlerBehaviour[] FindSteps()
        {
            return GetComponents<StepHandlerBehaviour>();
        }

        public virtual IStep[] Create(PipelineRunner runner)
        {
            var steps = new List<IStep>();
            for (int i = 0; i < Steps.Count; i++)
            {
                steps.AddRange(Steps[i].Create(runner));
            }
            return steps.ToArray();
        }

        /*public virtual IEnumerator Run(Action<IPipelineResult> result = null)
        {
            // TODO: Experiment with different casts.
            var steps = new IStep[Steps.Count];
            for (int i = 0; i < Steps.Count; i++)
            {
                steps[i] = Steps[i].Create();

                var stepResult = default(IStepResult);
                yield return steps[i].Run(value => stepResult = value);
            }
        }*/
    }
}