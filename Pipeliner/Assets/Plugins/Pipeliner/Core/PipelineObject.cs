using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// A Scriptable Object based Pipeline. Steps should be added to the Steps list.
    /// </summary>
    [CreateAssetMenu(fileName = "Pipeline", menuName = "Pipeliner/Pipeline", order = -1)]
    public class PipelineObject : ScriptableObject, IPipeline
    {
        public List<StepFactoryObject> Steps;

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