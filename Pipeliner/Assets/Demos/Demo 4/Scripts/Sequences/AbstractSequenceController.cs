using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Demos.Demo4
{
    [RequireComponent(typeof(PlayableDirector))]
    public abstract class AbstractSequenceController : MonoBehaviour
    {
        public PlayableDirector Director;

        protected virtual void OnValidate()
        {
            if (Director == null)
                Director = GetComponent<PlayableDirector>();
        }
    }
}

