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
        public List<StepFactoryBehaviour> Steps;

        public virtual StepFactoryBehaviour[] FindSteps()
        {
            return GetComponents<StepFactoryBehaviour>();
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
    }
}