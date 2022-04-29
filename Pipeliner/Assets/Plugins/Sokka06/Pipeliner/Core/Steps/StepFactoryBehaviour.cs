using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    /// <summary>
    /// A base class for MonoBehaviour Step factories.
    /// </summary>
    public abstract class StepFactoryBehaviour : MonoBehaviour, IStepFactory
    {
        protected const string MENU_PATH = "Pipeliner/Steps/";
        
        public abstract IStep[] Create();
    }
}