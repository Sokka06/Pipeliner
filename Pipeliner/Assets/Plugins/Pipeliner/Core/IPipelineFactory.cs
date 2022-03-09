using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public interface IPipelineFactory
    {
        /// <summary>
        /// Creates a Pipeline from steps.
        /// </summary>
        /// <returns></returns>
        Pipeline Create();
    }
}