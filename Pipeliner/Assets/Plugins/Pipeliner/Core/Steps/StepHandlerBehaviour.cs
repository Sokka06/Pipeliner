using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// A base class for MonoBehaviour step handlers.
    /// </summary>
    public abstract class StepHandlerBehaviour : MonoBehaviour, IStepHandler
    {
        protected const string MENU_PATH = "Pipeliner/Steps/";
        
        public abstract IStep[] Create(PipelineRunner runner);
    }
}