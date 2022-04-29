using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public static class Utils
    {
        /// <summary>
        /// Tries to find Pipeline Factory from given object.
        /// </summary>
        /// <returns></returns>
        public static IPipelineFactory FindPipeline(Object o)
        {
            // Try to directly cast Object to Pipeline.
            var pipeline = o as IPipelineFactory;
            
            // Cast failed, try to find Pipeline from a GameObject.
            if (pipeline == null)
            {
                if (o is GameObject go)
                {
                    pipeline = go.GetComponent<IPipelineFactory>();
                }
            }

            return pipeline;
        }
    }
}