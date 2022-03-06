using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public static class Utils
    {
        /// <summary>
        /// Tries to find Pipeline from given object.
        /// </summary>
        /// <returns></returns>
        public static IPipeline FindPipeline(Object o)
        {
            // Try to directly cast Object to Pipeline.
            var pipeline = o as IPipeline;
            
            // Cast failed, try to find Pipeline from a GameObject.
            if (pipeline == null)
            {
                if (o is GameObject go)
                {
                    pipeline = go.GetComponent<IPipeline>();
                }
            }

            return pipeline;
        }
    }
}