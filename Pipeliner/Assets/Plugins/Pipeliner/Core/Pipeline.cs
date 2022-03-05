using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public class Pipeline : IPipeline
    {
        private readonly IStep[] _steps;

        public Pipeline(IStep[] steps)
        {
            _steps = steps;
        }
        
        public IStep[] Create(PipelineRunner runner)
        {
            return _steps;
        }
    }
}