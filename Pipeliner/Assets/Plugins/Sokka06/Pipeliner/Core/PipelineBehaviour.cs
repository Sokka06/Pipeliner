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
    public class PipelineBehaviour : MonoBehaviour, IPipelineFactory
    {
        public StepFactoryBehaviour[] Steps = Array.Empty<StepFactoryBehaviour>();

        public virtual StepFactoryBehaviour[] FindSteps()
        {
            return GetComponents<StepFactoryBehaviour>();
        }

        public virtual Pipeline Create()
        {
            var steps = new List<IStep>();
            for (int i = 0; i < Steps.Length; i++)
            {
                steps.AddRange(Steps[i].Create());
            }
            return new Pipeline(steps.ToArray());
        }
    }
}