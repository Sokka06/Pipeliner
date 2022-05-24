using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public class Pipeline : IPipeline
    {
        private readonly IStep[] _steps;
        
        public IStep[] Steps => _steps;

        public Pipeline(IStep[] steps)
        {
            _steps = steps;
            for (int i = 0; i < _steps.Length; i++)
            {
                _steps[i].Pipeline = this;
            }
        }
        
        public virtual T GetStep<T>() where T : IStep
        {
            for (int i = 0; i < _steps.Length; i++)
            {
                if (_steps[i] is T step)
                    return step;
            }

            return default;
        }
        
        public virtual int GetSteps<T>(ref T[] steps) where T : IStep
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

        public int GetIndex(IStep step)
        {
            var index = -1;

            for (int i = 0; i < _steps.Length; i++)
            {
                if (_steps[i] != step)
                    continue;
                
                index = i;
                break;
            }
            
            return index;
        }
    }
}