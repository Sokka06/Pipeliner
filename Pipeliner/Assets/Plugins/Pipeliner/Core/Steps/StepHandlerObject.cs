using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// A base class for Scriptable Object step handlers.
    /// </summary>
    public abstract class StepHandlerObject : ScriptableObject, IStepHandler
    {
        protected const string MENU_PATH = "Pipeliner/Steps/";
        
        public abstract IStep[] Create(PipelineRunner runner);
    }
}