using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public class Pipeline : IPipeline
    {
        public IStep[] Steps => _steps;
        private readonly IStep[] _steps;

        public Pipeline(IStep[] steps)
        {
            _steps = steps;
        }
        
        public T GetStep<T>() where T : AbstractStep
        {
            for (int i = 0; i < _steps.Length; i++)
            {
                if (_steps[i] is T step)
                    return step;
            }

            return null;
        }
        
        public int GetSteps<T>(ref T[] steps) where T : AbstractStep
        {
            var count = 0;
            for (int i = 0; i < _steps.Length; i++)
            {
                if (_steps[i] is T step)
                {
                    steps[count] = step;
                    count++;
                }
                
                if (count >= steps.Length)
                    break;
            }
            
            return count;
        }
    }
}